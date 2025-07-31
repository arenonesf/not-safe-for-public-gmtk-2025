using UnityEngine;

public class Frame : MonoBehaviour
{
    private Tape _tape;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (_tape == null) return;
        _rigidbody2D.MovePosition(_rigidbody2D.position + _tape.TapeSpeed * Time.fixedDeltaTime * Vector2.right);
    }

    public void InitFrame(FrameData data, Tape tape)
    {
        _tape = tape;
        _spriteRenderer.sprite = data.FrameImage;
        _boxCollider2D.size = _spriteRenderer.bounds.size;
        _boxCollider2D.offset = _spriteRenderer.bounds.center - transform.position;
    }

    public void ReturnToStartPosition() 
    {
        _rigidbody2D.position = _tape.StartPosition.position;
    }
}
