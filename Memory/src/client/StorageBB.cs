using LogicAPI.Data;
using LogicWorld.Rendering.Components;

namespace Memory.Client {
    public class StorageBB : ComponentClientCode {

        protected override void Initialize() {
            if (Component.Data.CustomData == null) {
                ((IEditableComponentData)Component.Data).CustomData = new byte[255];
            }
        }
    }
}