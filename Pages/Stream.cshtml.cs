using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dartmedia.WebDemos.Services;

namespace Dartmedia.WebDemos.Pages
{
    public class StreamModel : PageModel
    {
        private readonly ILogger<StreamModel> _logger;

        private CollectionService _collnService;
        private KinesisStreamService _kinesisStreamService;
        private StreamProcessorService _streamProcessorService;
        private KinesisVideoStreamService _kinesisVideoStreamService;

        public StreamModel(ILogger<StreamModel> logger, 
            CollectionService collnService,  
            KinesisStreamService kinesisStreamService, 
            StreamProcessorService streamProcessorService,
            KinesisVideoStreamService kinesisVideoStreamService)
        {
            _logger = logger;

            _collnService = collnService;
            _kinesisStreamService = kinesisStreamService;
            _streamProcessorService = streamProcessorService;
            _kinesisVideoStreamService = kinesisVideoStreamService;

        }

        public CollectionService Collection { get; private set; }
        public KinesisStreamService DataStream { get; private set; }
        public KinesisVideoStreamService VideoStream { get; private set; }
        public StreamProcessorService StreamProcessor { get; private set; }

        public void OnGet()
        {
            Collection = _collnService;
            DataStream = _kinesisStreamService;
            VideoStream = _kinesisVideoStreamService;
            StreamProcessor = _streamProcessorService;
        }
    }
}
