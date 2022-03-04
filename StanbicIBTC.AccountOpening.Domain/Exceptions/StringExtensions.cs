namespace StanbicIBTC.AccountOpening.Domain;

public static class StringExtensions
{
    public static string AsNigerianPhoneNumber(this string phoneNumberString)
        {
            if (string.IsNullOrEmpty(phoneNumberString) || phoneNumberString.Length < 10 || phoneNumberString.StartsWith("234"))
                return phoneNumberString;
            if (phoneNumberString.StartsWith("0"))
                return new System.Text.StringBuilder("234").Append(phoneNumberString.Substring(1)).ToString();
            if (phoneNumberString.StartsWith("+234"))
                return phoneNumberString.Substring(1);
            return phoneNumberString;
        }

        public static string AsVirtualBankingPhoneNumber(this string phoneNumberString)
        {
            if (string.IsNullOrEmpty(phoneNumberString) || phoneNumberString.Length < 10 || phoneNumberString.StartsWith("234"))
                return phoneNumberString;
            if (phoneNumberString.StartsWith("0"))
                return new StringBuilder("234").Append(phoneNumberString.Substring(1)).ToString();
            if (phoneNumberString.StartsWith("+234"))
                return phoneNumberString.Substring(1);
            return phoneNumberString;
        }

        public static string ToJsonString(this object data)
        {
            try
            {
                return JsonConvert.SerializeObject(data);
            }
            catch{  return ""; }
        }

        public static T ToObject<T>(this string data)
        {
            try
            {
                if (typeof(T).FullName == "System.String")
                {
                    return (T)(object)data;
                }
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {
               return (T)(object)null;
            }
        }

        public static string RequestIP(this HttpContext context)
        {
            return context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public static string BytesToHexString(this byte[] bytes)
        {
            var stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                stringBuilder.Append(hex);
            }
            return stringBuilder.ToString();
        }
}