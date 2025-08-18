namespace Inject___launcher {
    internal class Log {
        public static string FormatLog(string doc) {
            return "[" + System.DateTime.Now.ToString("mm:ss") + "] " +doc ;
        }
    }
}
