using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.KinesisVideo;
using Amazon.KinesisVideo.Model;

namespace Dartmedia.WebDemos.Services
{
    public class KinesisVideoStreamService
    {
        private int _videoPersist = 24;
        private string _streamName = "streamvideo09";
        private IAmazonKinesisVideo _kinClient;

        public KinesisVideoStreamService()
        {
            _kinClient = new AmazonKinesisVideoClient();
        }

        public async Task<string> CreateNewVideoStream()
        {
            bool isStatusOK;

            CreateStreamRequest createStreamReq = new CreateStreamRequest
            {
                StreamName = _streamName,
                DataRetentionInHours = _videoPersist
            };

            CreateStreamResponse createStreamRes = await _kinClient.CreateStreamAsync(createStreamReq);
            isStatusOK = createStreamRes.HttpStatusCode == System.Net.HttpStatusCode.OK;

            if (isStatusOK)  
                return createStreamRes.StreamARN;

            return "";
        }
    }
}
