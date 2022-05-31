using Animators.MoveAnimations.BlendableMoveAnimations;
using ClickMania.Core.Game;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

        public int ID { get; private set; }
        public Vector2 Position { get; private set; }

        public void Init(TurnEntity turnEntity)
        {
            _turnEntity = turnEntity;
            _transform = GetComponent<Transform>();

            ID = -1;
            Position = _transform.position;
            _transform.localScale = _isHide ? Vector3.zero : Vector3.one;

            _input.OnMouseDownEvent += DoTurn;
        }

        private void DoTurn()
        {
            if(ID == -1) return;
            _turnEntity.Execute(ID);
        }

        public void SetBlockID(int id)
        {
            ID = id;
        }

        public Tween Move(float xCoordinate)
        {
            var toPosition = Position;
            toPosition.x = xCoordinate;
            return AnimateMove(toPosition, _moveAnimator);
        }

        public Tween Fall(float yCoordinate)
        {
            var toPosition = Position;
            toPosition.y = yCoordinate;
            return AnimateMove(toPosition, _fallAnimator);
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

        public Tween Hide()
        {
            if (_isHide) return default;
            _isHide = true; 
            _input.SetActiveInput(false);
            return _hideAnimator.StartAnimation(_transform, -Vector3.one);
        }

        public Tween Show()
        {
            if (_isHide == false) return default;
            _isHide = false;
            _input.SetActiveInput(true);
            return _showAnimator.StartAnimation(_transform, Vector3.one);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
        
        private Tween AnimateMove(Vector2 toPosition, BlendableTransformAnimator animator)
        {
            var moveVector = toPosition - Position;
            Position = toPosition;
            return animator.StartAnimation(_transform, moveVector);
        }
    }
}