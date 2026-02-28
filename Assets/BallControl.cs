using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class BallControl : MonoBehaviour
{
    private Rigidbody2D ball;
    private bool goToRed;
    private bool canMove = false;

    [Header("Força Inicial")]
    public float forceX = 8f;
    public float forceY = 6f;

    [Header("Controle de Velocidade")]
    public float minSpeed = 6f;
    public float maxSpeed = 12f;
    public float minAxisSpeed = 2.5f;

    private Vector3 startPosition;

    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        ResetBall();
        Invoke(nameof(GoBallRandom), 2f); // delay inicial
    }

    // ================= LANÇAMENTO =================

    void GoBallRandom()
{
    canMove = true;

    ball.linearVelocity = Vector2.zero;
    ball.angularVelocity = 0f;

    float x = Random.value < 0.5f ? -forceX : forceX;

    // Sempre positivo para subir
    float y = Random.Range(forceY * 0.6f, forceY);

    ball.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
}

    void GoBallToSide()
    {
        canMove = true;

        ball.linearVelocity = Vector2.zero;
        ball.angularVelocity = 0f;

        float x = Random.value < 0.5f ? -forceX : forceX;
        float y = goToRed ? forceY : -forceY;

        ball.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
    }

    // ================= CONTROLE DE VELOCIDADE =================

    void FixedUpdate()
    {
        if (!canMove) return;

        float speed = ball.linearVelocity.magnitude;

        if (speed < minSpeed)
            ball.linearVelocity = ball.linearVelocity.normalized * minSpeed;
        else if (speed > maxSpeed)
            ball.linearVelocity = ball.linearVelocity.normalized * maxSpeed;
    }

    void LateUpdate()
    {
        if (!canMove) return;

        Vector2 v = ball.linearVelocity;

        if (Mathf.Abs(v.x) < minAxisSpeed)
            v.x = Mathf.Sign(v.x == 0 ? Random.Range(-1f, 1f) : v.x) * minAxisSpeed;

        if (Mathf.Abs(v.y) < minAxisSpeed)
            v.y = Mathf.Sign(v.y == 0 ? Random.Range(-1f, 1f) : v.y) * minAxisSpeed;

        ball.linearVelocity = v;
    }

    // ================= COLISÃO =================

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.collider.CompareTag("Player")) return;

        Vector2 ballPos = transform.position;
        Vector2 malletPos = coll.transform.position;

        Vector2 direction = (ballPos - malletPos).normalized;
        float speed = ball.linearVelocity.magnitude;

        ball.linearVelocity = direction * speed;

        PlaySyntheticHit();

        
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            ball.AddForce(ball.linearVelocity.normalized * 0.05f, ForceMode2D.Impulse);
        }
    }

    // ================= RESET =================

    public void ResetBall()
    {
        CancelInvoke();
        canMove = false;
        ball.linearVelocity = Vector2.zero;
        ball.angularVelocity = 0f;
        transform.position = startPosition;
    }

    public void RestartGame(bool goToRedSide)
    {
        goToRed = goToRedSide;
        ResetBall();
        Invoke(nameof(GoBallToSide), 2f);
    }

    public void RestartFromCenter()
    {
        ResetBall();
        Invoke(nameof(GoBallRandom), 2f);
    }

    // ================= SOM =================

    void PlaySyntheticHit()
    {
        AudioSource audio = GetComponent<AudioSource>();

        int sampleRate = 44100;
        float duration = 0.08f;
        int samples = (int)(sampleRate * duration);

        float frequency = 1200f;
        float[] data = new float[samples];

        for (int i = 0; i < samples; i++)
        {
            float envelope = Mathf.Exp(-8f * i / samples);
            data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate) * envelope;
        }

        AudioClip clip = AudioClip.Create("Hit", samples, 1, sampleRate, false);
        clip.SetData(data, 0);

        audio.pitch = Random.Range(0.95f, 1.05f);
        audio.volume = 0.7f;
        audio.PlayOneShot(clip);
    }
}