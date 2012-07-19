using System;
using System.IO;

namespace Lokad.Cqrs.TapeStorage
{
    public class FileTransitionStore : ITransitionStore
    {
        private readonly string _rootPath;

        public FileTransitionStore(String rootPath)
        {
            _rootPath = rootPath;
        }

        public ITapeContainer GetContainer(string name)
        {
            string fullPath = Path.Combine(_rootPath, name);

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            return new FileTapeContainer(fullPath);
        }
    }
}