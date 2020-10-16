using System.Linq;

namespace data_handler_app.Shared
{
    public static class AliasMethods
    {
        public static string GetAlias(string companyName, Entities db, string aliasType)
        {
            int storeID = db.StoreCustomDatas.FirstOrDefault(x => x.StoreName == companyName).StoreID;

            int aliasTypeID = db.AliasTypes.FirstOrDefault(x => x.AliasName == aliasType).ID;

            return db.StoreAliases.FirstOrDefault(x => x.StoreID == storeID && x.AliasTypeID == aliasTypeID).Alias;
        }
    }
}