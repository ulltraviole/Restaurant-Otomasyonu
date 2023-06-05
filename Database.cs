using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Writers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace app
{
    internal class Database
    {
        IMongoDatabase App = null;

        public bool ConnectToApp()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.emirerenkara.me/");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                MongoClientSettings settings = MongoClientSettings.FromConnectionString(new StreamReader(response.GetResponseStream()).ReadToEnd());
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                MongoClient client = new MongoClient(settings);
                App = client.GetDatabase("App");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IMongoCollection<BsonDocument> GetCollection(string CollectionName)
        {
            try
            {
                IMongoCollection<BsonDocument> Collection = App.GetCollection<BsonDocument>(CollectionName);
                return Collection;
            }
            catch
            {
                return null;
            }
        }

        public BsonDocument GetStoreInformation()
        {
            BsonDocument StoreInformation = GetCollection("StoreInformation").Find(new BsonDocument()).FirstOrDefault();
            return StoreInformation;
        }

        public List<BsonDocument> GetListFromCollection(IMongoCollection<BsonDocument> Collection)
        {
            return Collection.Aggregate().ToList();
        }

        public BsonDocument CreateSessionDocument(string TableNumber, string Date, string StartTime, string Waiter)
        {
            BsonDocument Session = new BsonDocument
            {
                { "TableNumber", TableNumber },
                { "Date", Date },
                { "StartTime", StartTime },
                { "Waiter", Waiter },
                { "Price", "0" },
                { "BoughtProducts", new BsonArray()},
            };
            return Session;
        }

        public BsonDocument AuthStaff(string Username, string Password)
        {
            BsonDocument StaffData = null;
            List<BsonDocument> Staffs = GetListFromCollection(GetCollection("StaffAccounts"));
            foreach (BsonDocument Staff in Staffs)
            {
                if (Username == Staff["Username"] && Staff["Password"] == Password)
                {
                    StaffData = Staff;
                }
            }
            return StaffData;
        }

        public bool CheckStaff()
        {
            long AccountCount = GetCollection("StaffAccounts").Find(new BsonDocument()).CountDocuments();
            switch (AccountCount)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }

        public bool AddStaff(string Username, string Password, string Fullname, string Email, string AuthLevel)
        {
            try
            {
                IMongoCollection<BsonDocument> StaffCollection = GetCollection("StaffAccounts");
                BsonDocument Staff = new BsonDocument
                {
                    { "Username", Username },
                    { "Password" , Password },
                    { "Fullname" , Fullname },
                    { "Email" , Email },
                    { "AuthLevel" , AuthLevel }
                };
                StaffCollection.InsertOne(Staff);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddDish(string Name, string Price)
        {
            try
            {
                GetCollection("Dishes").InsertOne(new BsonDocument { { "Name", Name }, { "Price", Price } });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveDish(string Name)
        {
            try
            {
                foreach (BsonDocument Dish in GetDishes())
                {
                    if (Dish["Name"] == Name)
                    {
                        GetCollection("Dishes").DeleteOne(Dish);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<BsonDocument> GetDishes()
        {
            return GetListFromCollection(GetCollection("Dishes"));
        }

        public List<BsonDocument> GetStaffs()
        {
            return GetListFromCollection(GetCollection("StaffAccounts"));
        }

        public bool RemoveStaff(string Fullname)
        {
            try
            {
                foreach (BsonDocument Staff in GetStaffs())
                {
                    if (Staff["Fullname"] == Fullname)
                    {
                        GetCollection("StaffAccounts").DeleteOne(Staff);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool StartSession(string TableNumber, string Date, string StartTime, string Waiter)
        {
            try
            {
                IMongoCollection<BsonDocument> SessionCollection = GetCollection("Sessions");
                BsonDocument Session = CreateSessionDocument(TableNumber, Date, StartTime, Waiter);
                SessionCollection.InsertOne(Session);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTable(string TableNumber, string NewPrice, BsonDocument Product)
        {
            bool IsAnyActionTaken = false;
            IMongoCollection<BsonDocument> SessionCollection = GetCollection("Sessions");
            List<BsonDocument> Sessions = GetListFromCollection(SessionCollection);
            foreach (BsonDocument Session in Sessions)
            {
                if (Session["TableNumber"] == TableNumber)
                {
                    BsonDocument NewDoc = new BsonDocument
                    {
                        { "TableNumber",Session["TableNumber"] },
                        { "Date", Session["Date"] },
                        { "StartTime", Session["StartTime"] },
                        { "Waiter", Session["Waiter"] },
                        { "Price", NewPrice },
                        { "BoughtProducts", Session["BoughtProducts"].AsBsonArray.Add(Product)},
                };
                    SessionCollection.ReplaceOne(new BsonDocument { { "TableNumber", TableNumber } }, NewDoc);
                    IsAnyActionTaken = true;
                }
            }
            return IsAnyActionTaken;
        }

        public BsonDocument DeleteProduct(string TableNumber, string ProductName)
        {
            BsonDocument BoughtProductData = null;
            IMongoCollection<BsonDocument> SessionCollection = GetCollection("Sessions");
            List<BsonDocument> Sessions = GetListFromCollection(SessionCollection);
            foreach (BsonDocument Session in Sessions)
            {
                if (Session["TableNumber"] == TableNumber)
                {
                    BsonArray BoughtProducts = Session["BoughtProducts"].AsBsonArray;
                    foreach (BsonDocument BoughtProduct in BoughtProducts)
                    {
                        if (BoughtProduct["Name"] == ProductName)
                        {
                            BoughtProductData = BoughtProduct;
                            BoughtProducts.Remove(BoughtProduct);
                            int NewPrice = Int32.Parse(Session["Price"].AsString) - Int32.Parse(BoughtProduct["Price"].AsString);
                            BsonDocument NewDoc = new BsonDocument
                            {
                                { "TableNumber",Session["TableNumber"] },
                                { "Date", Session["Date"] },
                                { "StartTime", Session["StartTime"] },
                                { "Waiter", Session["Waiter"] },
                                { "Price", NewPrice.ToString()},
                                { "BoughtProducts", BoughtProducts},
                            };
                            SessionCollection.ReplaceOne(new BsonDocument { { "TableNumber", TableNumber } }, NewDoc);
                            break;
                        }
                    }
                }
            }
            return BoughtProductData;
        }

        public bool EndSession(string TableNumber, string EndTime)
        {
            bool IsAnyActionTaken = false;
            IMongoCollection<BsonDocument> SessionCollection = GetCollection("Sessions");
            List<BsonDocument> Sessions = GetListFromCollection(SessionCollection);
            foreach (BsonDocument Session in Sessions)
            {
                if (Session["TableNumber"] == TableNumber)
                {
                    BsonDocument OldSession = new BsonDocument
                        {
                            { "TableNumber",Session["TableNumber"] },
                            { "Date", Session["Date"] },
                            { "StartTime", Session["StartTime"] },
                            { "EndTime" , EndTime },
                            { "Waiter", Session["Waiter"] },
                            { "Price", Session["Price"] },
                            { "BoughtProducts", Session["BoughtProducts"] },
                        };
                    IMongoCollection<BsonDocument> OldSessionCollection = GetCollection("OldSessions");
                    SessionCollection.DeleteOne(new BsonDocument { { "TableNumber", TableNumber } });
                    OldSessionCollection.InsertOne(OldSession);
                    IsAnyActionTaken = true;
                }
            }
            return IsAnyActionTaken;
        }

        public BsonDocument FindSession(string TableNumber)
        {
            BsonDocument SessionData = null;
            List<BsonDocument> Sessions = GetListFromCollection(GetCollection("Sessions"));
            foreach (BsonDocument Session in Sessions)
            {
                if (Session["TableNumber"] == TableNumber)
                {
                    SessionData = Session;
                }
            }
            return SessionData;
        }

        public void UpdateStoreInformation(String StoreName, int StoreTableCount)
        {
            IMongoCollection<BsonDocument> StoreInformationCollection = GetCollection("StoreInformation");
            BsonDocument StoreInformation = StoreInformationCollection.Find(new BsonDocument { }).FirstOrDefault();
            StoreInformationCollection.DeleteOne(StoreInformation);
            StoreInformationCollection.InsertOne(new BsonDocument
            {
                { "StoreName", StoreName },
                { "StoreTableCount", StoreTableCount }
            });
        }
    }
}