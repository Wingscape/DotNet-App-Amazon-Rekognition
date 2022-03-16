using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace Dartmedia.WebDemos.Services
{
    public class StreamProcessorService
    {
        private AmazonRekognitionClient _rekClient;

        public StreamProcessorService()
        {
            _rekClient = new AmazonRekognitionClient();          
        }

        public async Task CreateNewStream(string videoARN, string dataARN)
        {
            KinesisVideoStream videoStream = new KinesisVideoStream{ Arn = videoARN };
            KinesisDataStream dataStream = new KinesisDataStream{ Arn = dataARN };

            FaceSearchSettings searchSettings = new FaceSearchSettings
            {
                CollectionId = "090909",
                FaceMatchThreshold = 85.5f
            };

            StreamProcessorInput procInput = new StreamProcessorInput
            {
                KinesisVideoStream = videoStream
            };

            StreamProcessorOutput procOutput = new StreamProcessorOutput
            {
                KinesisDataStream = dataStream
            };

            StreamProcessorSettings procSettings = new StreamProcessorSettings
            {
                FaceSearch = searchSettings
            };

            CreateStreamProcessorRequest createProcReq = new CreateStreamProcessorRequest
            {
                Name = "StreamFaceRecog",
                Input = procInput,
                Output = procOutput,
                RoleArn = "",
                Settings = procSettings
            };

            CreateStreamProcessorResponse createProcRes = await _rekClient.CreateStreamProcessorAsync(createProcReq);
            Console.WriteLine("The ARN for the newly create stream processor is " + createProcRes.StreamProcessorArn);
        }

        public async Task StartStream()
        {
            StartStreamProcessorRequest streamProcReq = new StartStreamProcessorRequest
            {
                Name = "StreamFaceRecog"
            };

            await _rekClient.StartStreamProcessorAsync(streamProcReq);
            Console.WriteLine("Stream Processor " + "StreamFaceRecog" + " started.");
        }

        public async Task StopDeleteStream()
        {
            StopStreamProcessorRequest stopProcReq = new StopStreamProcessorRequest{ Name = "StreamFaceRecog" };
            await _rekClient.StopStreamProcessorAsync(stopProcReq);

            DeleteStreamProcessorRequest delProcReq = new DeleteStreamProcessorRequest{ Name = "StreamFaceRecog" };
            await _rekClient.DeleteStreamProcessorAsync(delProcReq);

            Console.WriteLine("Stream Processor " + "StreamFaceRecog" + " deleted.");
        }
    }
}
