using LogicAPI.Server.Components;

namespace Memory.Server {
    public class StorageBB : LogicComponent {

        private bool readLock = false;
        private bool writeLock = false;
        private bool eraseLock = false;

        protected override void DoLogicUpdate() {
            int address = 0;
            for (int i = 0; i < 8; i++) {
                address <<= 1;
                if (base.Inputs[8 + i].On) address |= 1;
            }

            bool read = base.Inputs[16].On;
            bool write = base.Inputs[17].On;
            bool erase = base.Inputs[18].On;

            if (!read) readLock = false;
            if (!write) writeLock = false;
            if (!erase) eraseLock = false;

            if (!eraseLock && erase) {
                eraseLock = true;
                for (int i = 0; i < 255; i++) {
                    ComponentData.CustomData[i] = 0;
                }
                return;
            }

            if (!writeLock && write) {
                writeLock = true;
                byte input_data = 0;
                for (int i = 0; i < 8; i++) {
                    input_data <<= 1;
                    if (base.Inputs[i].On) input_data |= 1;
                }
                ComponentData.CustomData[address] = input_data;
            }

            if (!readLock && read) {
                readLock = true;
                byte output_data = ComponentData.CustomData[address];
                for (int i = 0; i < 8; i++) {
                    base.Outputs[i].On = (output_data & (1 << (7 - i))) != 0;
                }
            }
        }

        public override void Dispose() {
            base.Dispose();
        }
    }
}