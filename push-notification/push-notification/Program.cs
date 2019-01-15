using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPush;

namespace push_notification
{
    class Program
    {
        static void Main(string[] args)
        {
            var pushEndpoint = @"https://updates.push.services.mozilla.com/wpush/v2/gAAAAABcPYyEx7EN4bI8suXnzcCB94xX6kd0_m4ggu1qdpGfdwkmMw5kWUq_Kacjy_R-j1iyp_OzuvILnizH5dbFJWsUVqH3uHYoz8c9Ya3wLIq39l9h_ke8gp-3gj6GKS5Se2yWQnxbxaiYes8s1c6TZzQFjiX9VlWz3Jou2pQ7N-alab1thcI";
            var p256dh = @"BM-wGN-lWJWrZ_EinsQBjay_Os8flZTfhN4xaBqlw1aJ_usc9qJi7vMf5wHFZYqwatyt0BCRnfp6PdboE92m95o";
            var auth = @"byXCU0iZ_d12HdE9f23WvA";

            var subject = @"mailto:example@example.com";
            var publicKey = @"BM19ABrxlF90WXluoBYTPuOf53JzbgXKVaiMJAVo6HELcb01UfG6h7_Y0GYhJzVcEYWLirOwvCuUTY0dGCEnOgU";
            var privateKey = @"rZjgAUeFmMVx9AhGZSFyM264QKEDLnKcbViCBeRUJpQ";

            var subscription = new PushSubscription(pushEndpoint, p256dh, auth);
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);
            //var gcmAPIKey = @"[your key here]";
            int i = 0;
            var webPushClient = new WebPushClient();
            while (true)
            {
                Console.WriteLine("Push: "+ i);
                try
                {
                    webPushClient.SendNotification(subscription, "Push: "+i, vapidDetails);
                }
                catch (WebPushException exception)
                {
                    Console.WriteLine("Http STATUS code" + exception.StatusCode);
                }
                i++;
                Task.Delay(5000).Wait();
            }
        }
    }
}
