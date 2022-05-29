using Animators.MoveAnimations.BlendableMoveAnimations;
using ClickMania.Core.Game;
using SDK.ColliderClickDetection;
using UnityEngine;

namespace ClickMania.View.Block
{
    public class BlockView : MonoBehaviour, IBlockView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ColliderMouseDetector _input;
        [SerializeField] private BlendableTransformAnimator _fallAnimator;
        [SerializeField] private BlendableTransformAnimator _moveAnimator;
        [SerializeField] private BlendableScaleAnimator _showAnimator;
        [SerializeField] private BlendableScaleAnimator _hideAnimator;

        private bool _isHide;
        private Transform _transform;
        private TurnEntity _turnEntity;

        public int BlockID { get; private set; }
        public Vector2 Position { get; private set; }

        public void Init(TurnEntity turnEntity)
        {
            _turnEntity = turnEntity;
            _transform = GetComponent<Transform>();

            BlockID = -1;
            Position = _transform.position;
            _transform.localScale = _isHide ? Vector3.zero : Vector3.one;

            _input.OnMouseDownEvent += DoTurn;
        }

        private void DoTurn()
        {
            if(BlockID == -1) return;
            _turnEntity.Execute(BlockID);
        }

        public void SetBlockID(int id)
        {
            BlockID = id;
        }

        public void Move(float xCoordinate)
        {
            var toPosition = Position;
            toPosition.x = xCoordinate;
            AnimateMove(toPosition, _moveAnimator);
        }

        public void Fall(float yCoordinate)
        {
            var toPosition = Position;
            toPosition.y = yCoordinate;
            AnimateMove(toPosition, _fallAnimator);
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
            _transform.position = position;
        }

        public void SetScale(Vector2 scale)
        {
            _transform.localScale = scale;
        }

        public void Hide()
        {
            if (_isHide) return;
            _hideAnimator.StartAnimation(_transform, -Vector3.one);
            _isHide = true;
        }

        public void Show()
        {
            if (_isHide == false) return;
            _showAnimator.StartAnimation(_transform, Vector3.one);
            _isHide = false;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void AnimateMove(Vector2 toPosition, BlendableTransformAnimator animator)
        {
            var moveVector = toPosition - Position;
            animator.StartAnimation(_transform, moveVector);
            Position = toPosition;
        }
    }
}