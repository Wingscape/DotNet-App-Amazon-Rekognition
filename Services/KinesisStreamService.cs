using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace Dartmedia.WebDemos.Services
{
    public class KinesisStreamService
    {
        private int _shardCount = 4;
        private string _streamName = "AmazonRekognition";
        private IAmazonKinesis _kinClient;

        public KinesisStreamService()
        {
            _kinClient = new AmazonKinesisClient();
        }

        public async Task<string> CreateNewStream()
        {
            bool isStatusOK;

            CreateStreamRequest createStreamReq = new CreateStreamRequest
            {
                StreamName = _streamName,
                ShardCount = _shardCount
            };

            CreateStreamResponse createStreamRes = await _kinClient.CreateStreamAsync(createStreamReq);
            isStatusOK = createStreamRes.HttpStatusCode == System.Net.HttpStatusCode.OK;

            if (isStatusOK)
            {
                DescribeStreamRequest descStreamReq = new DescribeStreamRequest{ StreamName = _streamName };
                DescribeStreamResponse descStreamRes = await _kinClient.DescribeStreamAsync(descStreamReq);

                return descStreamRes.StreamDescription.StreamARN;
            }

            return "";
        }
    }
}
