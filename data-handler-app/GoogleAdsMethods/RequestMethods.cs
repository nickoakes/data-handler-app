using Google.Ads.GoogleAds.V4.Services;
using Google.Protobuf.Collections;

namespace data_handler_app.GoogleAdsMethods
{
    public static class RequestMethods
    {
        public static RepeatedField<GoogleAdsRow> SearchRequest(string customerID, string query, GoogleAdsServiceClient googleAdsService)
        {
            RepeatedField<GoogleAdsRow> results = new RepeatedField<GoogleAdsRow>();

            // Issue a search request.
            googleAdsService.SearchStream(customerID, query,
                delegate (SearchGoogleAdsStreamResponse resp)
                {
                    for (int i = 0; i < resp.Results.Count; i++)
                    {
                        results.Add(resp.Results[i]);
                    }
                }
            );

            return results;
        }
    }
}