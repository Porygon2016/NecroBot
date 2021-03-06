﻿#region using directives

using System.Threading.Tasks;
using PoGo.NecroBot.Logic.Tasks;

#endregion

namespace PoGo.NecroBot.Logic.State
{
    public class FarmState : IState
    {
        public async Task<IState> Execute(Context ctx, StateMachine machine)
        {
            await RenamePokemonTask.Execute(ctx, machine);

            await DisplayPokemonStatsTask.Execute(ctx, machine);

            if (ctx.LogicSettings.EvolveAllPokemonAboveIv || ctx.LogicSettings.EvolveAllPokemonWithEnoughCandy)
            {
                await EvolvePokemonTask.Execute(ctx, machine);
            }

            if (ctx.LogicSettings.TransferDuplicatePokemon)
            {
                await TransferDuplicatePokemonTask.Execute(ctx, machine);
            }

            await RecycleItemsTask.Execute(ctx, machine);

            if (ctx.LogicSettings.UseGpxPathing)
            {
                await FarmPokestopsGpxTask.Execute(ctx, machine);
            }
            else
            {
                await FarmPokestopsTask.Execute(ctx, machine);
            }

            await Task.Delay(10000);

            return this;
        }
    }
}