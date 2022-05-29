using System;
using ClickMania.Core.Areas.Search;
using ClickMania.Core.Blocks;
using ClickMania.Core.Blocks.BlockGroupUpdating;
using ClickMania.Core.Features.BlockFalling;
using ClickMania.Core.Features.CleanEmptyColumns;

namespace ClickMania.Core.Game
{
    public class TurnEntity
    {
        private readonly ICleanEmptyCollumns _emptyCollumnsCleaner;
        private readonly IFallBlocks _blocksFallFeature;
        private readonly IEndGameConditions _endGameChecker;
        private readonly IUpdateBlockGroup _blockGroupUpdater;
        private readonly IFindBlock _blockFinder;

        public event Action OnTurn = delegate { };
        
        public TurnEntity(IFallBlocks blocksFallFeature, ICleanEmptyCollumns emptyCollumnsCleaner, IEndGameConditions endGameChecker, IUpdateBlockGroup blockGroupUpdater, IFindBlock blockFinder)
        {
            _blockFinder = blockFinder;
            _blocksFallFeature = blocksFallFeature;
            _emptyCollumnsCleaner = emptyCollumnsCleaner;
            _endGameChecker = endGameChecker;
            _blockGroupUpdater = blockGroupUpdater;
        }

        public void Execute(int blockID)
        {
            if(_blockFinder.TryFindBlockInArea(blockID, out var block) == false) return;
            
            var group = block.Group;
            
            if(group.Length < 2) return;
            
            DestroyGroup(group);
            _blocksFallFeature.Execute();
            _emptyCollumnsCleaner.Execute();
            _endGameChecker.Check();
            _blockGroupUpdater.UpdateGroups();
            
            OnTurn.Invoke();
        }

        private static void DestroyGroup(IBlock[] group)
        {
            for (var i = 0; i < group.Length; i++)
            {
                group[i].Destroy();
            }
        }
    }
}