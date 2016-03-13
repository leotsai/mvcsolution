
namespace MvcSolution
{
    public static class IoHelper
    {
        public static void CreateDirectoryIfNotExists(string directory)
        {
            lock (string.Intern(directory))
            {
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
        }
    }
}
