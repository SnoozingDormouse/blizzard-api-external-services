namespace BlizzardData.Domain.Entities
{
    public class BattlePetBattlePetAbility
    {
        public int BattlePetId { get; set; }

        public int BattlePetAbilityId { get; set; }

        public int Slot { get; set; }

        public int SlotColumn { get; set; }

        public int RequiredLevel { get; set; }
    }
}
