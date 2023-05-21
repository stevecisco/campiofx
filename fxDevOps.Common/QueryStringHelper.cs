namespace fxDevOps.Common
{
    public class QueryStringHelper
    {
        public static Tuple<bool, string> TryGetQueryStringValue(string queryString, string param)
        {
            var query = System.Web.HttpUtility.ParseQueryString(queryString);
            if (query != null && query.AllKeys.Contains(param))
            {
                return new Tuple<bool, string>(true, query[param]);
            }
            else
            {
                return new Tuple<bool, string>(false, null);
            }
        }
    }
}