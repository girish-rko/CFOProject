using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Services
{
    public class CfoEvent
    {
        private string _message;
        private IDictionary<string, string> _tags;

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:SharpRaven.Data.SentryEvent" /> class.</summary>
        /// <param name="exception">The <see cref="P:SharpRaven.Data.SentryEvent.Exception" /> to capture.</param>
        public CfoEvent(Exception exception) : this()
        {
            Exception = exception;
            Level = CfoErrorLevel.Error;
        }

        /// <summary>Initializes a new instance of the <see cref="T:SharpRaven.Data.SentryEvent" /> class.</summary>
        /// <param name="message">The message to capture.</param>
        public CfoEvent(string message) : this()
        {
            Message = message;
        }

        /// <summary>Prevents a default instance of the <see cref="T:SharpRaven.Data.SentryEvent" /> class from being created.</summary>
        private CfoEvent()
        {
            Tags = new Dictionary<string, string>();
        }

        /// <summary>Gets the <see cref="P:SharpRaven.Data.SentryEvent.Exception" /> to capture.</summary>
        /// <value>The <see cref="P:SharpRaven.Data.SentryEvent.Exception" /> to capture.</value>
        public Exception Exception { get; }

        /// <summary>Gets extra metadata to send with the captured <see name="Exception" /> or <see cref="P:SharpRaven.Data.SentryEvent.Message" />.</summary>
        /// <value>
        /// The extra metadata to send with the captured <see name="Exception" /> or <see cref="P:SharpRaven.Data.SentryEvent.Message" />.
        /// </value>
        public object Extra { get; set; }

        /// <summary>Gets or Sets trail with <see cref="T:SharpRaven.Data.Breadcrumb" />.</summary>
        //public List<TronixBreadcrumb> Breadcrumbs { get; set; } = new List<TronixBreadcrumb>();

        /// <summary>
        /// Gets the <see cref="T:SharpRaven.Data.ErrorLevel" /> of the captured <see name="Exception" />
        /// or <see cref="P:SharpRaven.Data.SentryEvent.Message" />. Default: <see cref="F:SharpRaven.Data.ErrorLevel.Error" />.
        /// </summary>
        /// <value>
        /// The <see cref="T:SharpRaven.Data.ErrorLevel" /> of the captured <see name="Exception" />
        /// or <see cref="P:SharpRaven.Data.SentryEvent.Message" />. Default: <see cref="F:SharpRaven.Data.ErrorLevel.Error" />.
        /// </value>
        public CfoErrorLevel Level { get; set; }

        /// <summary>Gets the optional message to capture instead of the default <see cref="T:Exception.Message" />.</summary>
        /// <value>
        /// The optional message to capture instead of the default <see cref="T:Exception.Message" />.
        /// </value>
        public string Message
        {

            get
            {
                return _message ?? Exception?.Message?.Replace("\r\n", " ");
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>Gets the tags to annotate the captured <see name="Exception" /> or <see cref="P:SharpRaven.Data.SentryEvent.Message" /> with.</summary>
        /// <value>
        /// The tags to annotate the captured <see name="Exception" /> or <see cref="P:SharpRaven.Data.SentryEvent.Message" /> with.
        /// </value>
        public IDictionary<string, string> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value ?? new Dictionary<string, string>();
            }
        }
    }
}

