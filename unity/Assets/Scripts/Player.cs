using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using System.Collections;

public class Player : MonoBehaviour
{

    public float speed = 1.5f;

    private Rigidbody2D rigidBody2D;
    private Vector2 movement;
    private PlayerData playerData;
    private bool isGameOver;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        isGameOver = false;
        // playerData = GetComponent<PlayerData>();
        playerData = new PlayerData();
        playerData.plummie_tag = "atacke";
        StartCoroutine(Download(playerData.plummie_tag, result => {
            Debug.Log(result);
        }));
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        movement = new Vector2(h * speed, v * speed);
    }

    void FixedUpdate() {
        rigidBody2D.velocity = movement;

        if(rigidBody2D.position.x > 24.0f && isGameOver == false) {
            StartCoroutine(Upload(playerData.Stringify(), result => {
                Debug.Log(result);
            }));
            isGameOver = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        playerData.collisions++;
    }

    IEnumerator Download(string id, System.Action<PlayerData> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/plummies/" + id))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(null);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(PlayerData.Parse(request.downloadHandler.text));
                }
            }
        }
    }

    IEnumerator Upload(string profile, System.Action<bool> callback = null)
    {
        using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/plummies", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(profile);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if(callback != null) {
                    callback.Invoke(false);
                }
            }
            else
            {
                // Debug.Log(request.downloadHandler.text);
                if(callback != null) {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }

}
