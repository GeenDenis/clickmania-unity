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

        public UniTask Move(float xCoordinate)
        {
            var toPosition = Position;
            toPosition.x = xCoordinate;
            return AnimateMove(toPosition, _moveAnimator);
        }

        public UniTask Fall(float yCoordinate)
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

        public UniTask Hide()
        {
            if (_isHide) return UniTask.CompletedTask;
            var tween = _hideAnimator.StartAnimation(_transform, -Vector3.one);
            _isHide = true;
            return tween.AsyncWaitForCompletion().AsUniTask();
        }

        public UniTask Show()
        {
            if (_isHide == false) return UniTask.CompletedTask;
            var tween = _showAnimator.StartAnimation(_transform, Vector3.one);
            _isHide = false;
            return tween.AsyncWaitForCompletion().AsUniTask();
        }

        public UniTask Destroy()
        {
            var task = Hide();
            _input.SetActiveInput(false);
            DestroyOnCompleteTask(task);
            return task;
        }

        public void DestroyImmediate()
        {
            Destroy(gameObject);
        }
        
        private UniTask AnimateMove(Vector2 toPosition, BlendableTransformAnimator animator)
        {
            var moveVector = toPosition - Position;
            var tween = animator.StartAnimation(_transform, moveVector);
            Position = toPosition;
            return tween.AsyncWaitForCompletion().AsUniTask();
        }

        private async void DestroyOnCompleteTask(UniTask task)
        {
            await task;
            Destroy(gameObject);
        }
    }
}