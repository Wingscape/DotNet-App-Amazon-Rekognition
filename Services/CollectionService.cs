using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace Dartmedia.WebDemos.Services
{
    public class CollectionService
    {
        private string _collnId = "090909";
        private AmazonRekognitionClient _rekogClient;

        public CollectionService()
        {
            _rekogClient = new AmazonRekognitionClient();
        }

        public async Task CreateCollection()
        {
            CreateCollectionRequest createCollnReq = new CreateCollectionRequest
            {
                CollectionId = _collnId,
            };

            await _rekogClient.CreateCollectionAsync(createCollnReq);
        }

        public async Task AddFaces()
        {
            string photo = @"C:\Users\dartmedia\Pictures\Screenshots\Screenshot8.png";
            Image image = new Image();

            using (FileStream f = new FileStream(photo, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[f.Length];
                f.Read(data, 0, (int)f.Length);

                image.Bytes = new MemoryStream(data);
            }

            IndexFacesRequest indexFaceReq = new IndexFacesRequest()
            {
                Image = image,
                CollectionId = _collnId,
                ExternalImageId = "Naufal.png",
                DetectionAttributes = new List<string>(){ "ALL" }
            };

            await _rekogClient.IndexFacesAsync(indexFaceReq);
        }
    }
}
