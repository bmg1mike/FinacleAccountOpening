using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace StanbicIBTC.AccountOpening.Service;

public class Util
{
    public static string GetXmlTagValue(string xmlObject, string element, string namespacePrefix = "", bool ignoreCase = true)
    {
        try
        {
            xmlObject = xmlObject.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("&lt;", "<").Replace("&gt;", ">");
            var pattern = string.IsNullOrEmpty(namespacePrefix) ? $@"<{element}>.+</{element}>" : $@"<{namespacePrefix}:{element}>.+</{namespacePrefix}:{element}>";
            var matches = ignoreCase ? Regex.Matches(xmlObject, pattern, RegexOptions.IgnoreCase) : Regex.Matches(xmlObject, pattern);
            var matchCount = matches.Count;
            if (matchCount < 1)
                return "";
            var openingTag = string.IsNullOrEmpty(namespacePrefix) ? $"<{element}>" : $"<{namespacePrefix}:{element}>";
            var closingTag = string.IsNullOrEmpty(namespacePrefix) ? $"</{element}>" : $"</{namespacePrefix}:{element}>";
            var value = matches[0].Value;
            value = value.ToString().Replace(openingTag, "").Replace(closingTag, "")?.Trim();
            return value;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string GetTagValue(string xmlObject, string element, string namespacePrefix = "", bool retainTag = false, bool ignoreCase = true)
    {
        try
        {
            xmlObject = xmlObject.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("&lt;", "<").Replace("&gt;", ">");
            var openingTag = string.IsNullOrEmpty(namespacePrefix) ? $"<{element}>" : $"<{namespacePrefix}:{element}>";
            var closingTag = string.IsNullOrEmpty(namespacePrefix) ? $"</{element}>" : $"</{namespacePrefix}:{element}>";
            var pattern = $@"{openingTag}.+{closingTag}";
            var matches = ignoreCase ? Regex.Matches(xmlObject, pattern, RegexOptions.IgnoreCase) : Regex.Matches(xmlObject, pattern);
            string tagContent = string.Empty;
            var matchCount = matches.Count;
            if (matchCount < 1)
            {
                if (xmlObject.Contains(openingTag))
                {
                    var firstIndexOfOTag = xmlObject.IndexOf(openingTag);
                    var indexOfClosingTag = xmlObject.IndexOf(closingTag);
                    var textLength = xmlObject.Length;
                    tagContent = xmlObject.Substring(firstIndexOfOTag, (indexOfClosingTag - firstIndexOfOTag) + closingTag.Length);
                }
                if (string.IsNullOrEmpty(tagContent))
                    return string.Empty;
            }
            else
            {
                tagContent = matches[0].Value;
            }
            if (retainTag)
                return tagContent;
            var value = tagContent?.ToString().Replace(openingTag, "").Replace(closingTag, "")?.Trim();
            return value;
        }
        catch (Exception)
        {
            //Log.Error(new Exception($"Encountered an error while attempting to get XMLElement with name {element} from Xmlobject \n: {exception}"));
            return "";
        }
    }

    public static string GetFirstTagValue(string xmlObject, string element, string namespacePrefix = "", bool retainTag = false, bool ignoreCase = true)
    {
        try
        {
            xmlObject = xmlObject.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("&lt;", "<").Replace("&gt;", ">");
            var openingTag = string.IsNullOrEmpty(namespacePrefix) ? $"<{element}>" : $"<{namespacePrefix}:{element}>";
            var closingTag = string.IsNullOrEmpty(namespacePrefix) ? $"</{element}>" : $"</{namespacePrefix}:{element}>";
            string tagContent = string.Empty;
            if (xmlObject.Contains(openingTag))
            {
                var firstIndexOfOTag = xmlObject.IndexOf(openingTag);
                var indexOfClosingTag = xmlObject.IndexOf(closingTag);
                var textLength = xmlObject.Length;
                tagContent = xmlObject.Substring(firstIndexOfOTag, (indexOfClosingTag - firstIndexOfOTag) + closingTag.Length);
            }
            if (string.IsNullOrEmpty(tagContent))
                return string.Empty;
            if (retainTag)
                return tagContent;
            var value = tagContent?.ToString().Replace(openingTag, "").Replace(closingTag, "")?.Trim();
            return value;
        }
        catch (Exception)
        {
            //Log.Error(new Exception($"Encountered an error while attempting to get XMLElement with name {element} from Xmlobject \n: {exception}"));
            return "";
        }
    }

    public static T DeserializeXML<T>(string objectData)
    {
        objectData = objectData.Replace("\n", "");
        var serializer = new XmlSerializer(typeof(T));
        object result;
        using (TextReader reader = new StringReader(objectData))
        {
            result = serializer.Deserialize(reader);
        }
        return (T)result;

    }

    public static string SerializeXML(object objectInstance, string _prefix = "", string _namespace = "", bool _header = false)
    {
        string txt = "";
        var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

        if (!string.IsNullOrEmpty(_prefix))
        {
            emptyNamepsaces = new XmlSerializerNamespaces();
            emptyNamepsaces.Add(_prefix, _namespace);
        }
        var serializer = new XmlSerializer(objectInstance.GetType());
        var settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = _header;
        settings.Encoding = new UTF8Encoding(false);
        settings.ConformanceLevel = ConformanceLevel.Document;
        var memoryStream = new MemoryStream();
        using (var writer = XmlWriter.Create(memoryStream, settings))
        {
            serializer.Serialize(writer, objectInstance, emptyNamepsaces);
            txt = Encoding.UTF8.GetString(memoryStream.ToArray());
        }
        return txt;
    }


    public static string TimeStampCode(string prefix = "")
    {
        Thread.Sleep(1);
        string stamp = DateTime.Now.ToString("yyMMddHHmmssffffff");
        long num = long.Parse(stamp);
        var g = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
        return prefix + num + g;
    }


    public static long CurrentDateTimeLong()
    {
        return Convert.ToInt64(CurrentDateTime().ToString("yyyyMMddHHmmss"));
    }

    public static DateTime CurrentDateTime()
    {
        return DateTime.UtcNow;
    }

    public static string CurrentDateTimeString()
    {
        return CurrentDateTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff");
    }

    public static DateTime ToDateTime(string v)
    {
        var a = DateTime.MinValue;
        var b = DateTime.TryParse(v, out a);
        return a;
    }

    public static string GenerateRandomNumbers(int cnt)
    {
        string[] keys = new string[]
        {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
        };
        Random rand = new Random();
        string txt = "";
        for (int i = 0; i < cnt; i++)
        {
            txt += keys[rand.Next(0, 9)];
        }
        return txt;
    }

    public static string PaymentId()
    {
        return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
    }

    public static string GetTelcoName(string network)
    {
        switch (network)
        {
            case "621-30": return "mtn";
            case "621-20": return "airtel";
            case "621-50": return "glo";
            case "621-60": return "9mobile";
            default: return "";
        }
    }

    public static string MobileBase(string mobile)
    {
        return mobile.Substring(mobile.Length - 10, 10);
    }
}
