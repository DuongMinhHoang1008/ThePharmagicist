using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ThangChibaGPT
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Endpoint for the FastAPI server")]
        private string endPoint = "https://27ad-2405-4802-2448-23c0-28f5-72cd-bb03-9321.ngrok-free.app/chat_with_knowledge_base";

        public void Send(string chatContent, AIChatController controller)
        {
            controller.OnSubmitChat(chatContent);
            StartCoroutine(ChatWithAI(chatContent, controller));
        }

        private IEnumerator ChatWithAI(string query, AIChatController controller)
        {
            var requestUrl = endPoint;
            var form = new WWWForm();
            form.AddField("query", query);
            var request = UnityWebRequest.Post(requestUrl, form);

            request.SetRequestHeader("Accept", "text/event-stream");

            var downloadHandler = new StreamingDownloadHandler(controller);
            request.downloadHandler = downloadHandler;

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {request.error}");
                controller.OnReceiveResponse("Error: Unable to connect to the AI service.");
            }
        }

        private class StreamingDownloadHandler : DownloadHandlerScript
        {
            private readonly AIChatController controller;
            private string dataString = "";
            private string deltaContent = "";

            public StreamingDownloadHandler(AIChatController controller)
            {
                this.controller = controller;
            }

            protected override bool ReceiveData(byte[] data, int dataLength)
            {
                dataString = Encoding.UTF8.GetString(data, 0, dataLength);
                var responseData = dataString.Split(new[] { "data: " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var response in responseData)
                {
                    if (response.Trim() == "[DONE]")
                    {
                        controller.OnReceiveResponse(deltaContent);
                        break;
                    }

                    var chatCompletion = JsonUtility.FromJson<ChatCompletionResponse>(response);

                    foreach (var choice in chatCompletion.choices)
                    {
                        if (choice.delta == null || string.IsNullOrEmpty(choice.delta.content)) continue;
                        deltaContent += choice.delta.content;
                        controller.OnReceiveChunkResponse(deltaContent);
                        break; // only consider the first delta with content field
                    }
                }

                return base.ReceiveData(data, dataLength);
            }
        }
    }
}
