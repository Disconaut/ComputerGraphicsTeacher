using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace CGTeacherShared.Book
{
    public class BookLoader
    {
        private const string BookPagesPrefix = "CGTeacherShared.Book";

        private static readonly Lazy<BookLoader> Instance = new Lazy<BookLoader>(() => new BookLoader());

        private readonly IDictionary<string, string> _bookPages;
        private readonly Assembly _assembly;

        public ICollection<string> BookPagesName => _bookPages.Keys;

        private BookLoader()
        {
            _bookPages = new Dictionary<string, string>();

            _assembly = Assembly.GetAssembly(GetType());
            var resourceNames = _assembly
                .GetManifestResourceNames()
                .Where(x => x.StartsWith(BookPagesPrefix));

            foreach (var name in resourceNames)
            {
                var resourceStream = _assembly.GetManifestResourceStream(name);
                if(resourceStream == null) continue;
                var document = new MarkdownDocument();
                using (var stream = new StreamReader(resourceStream))
                {
                    document.Parse(stream.ReadToEnd());
                }

                var header = document.Blocks.FirstOrDefault(x => x is HeaderBlock h && h.HeaderLevel == 1);
                _bookPages.Add(header?.ToString() ?? name, name);
            }
        }

        public Stream GetPageStream(string bookPageName)
        {
            return _bookPages.ContainsKey(bookPageName)
                ? _assembly.GetManifestResourceStream(_bookPages[bookPageName])
                : null;
        }

        public static BookLoader GetInstance()
        {
            return Instance.Value;
        }
    }
}
