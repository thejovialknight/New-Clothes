using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.BehaviourTrees
{

    /// BehaviourTrees are used to create advanced AI and logic based on simple rules.
    [GraphInfo(
        packageName = "NodeCanvas",
        docsURL = "https://nodecanvas.paradoxnotion.com/documentation/",
        resourcesURL = "https://nodecanvas.paradoxnotion.com/downloads/",
        forumsURL = "https://nodecanvas.paradoxnotion.com/forums-page/"
        )]
    [CreateAssetMenu(menuName = "ParadoxNotion/NodeCanvas/Behaviour Tree Asset")]
    public class BehaviourTree : Graph
    {

        ///----------------------------------------------------------------------------------------------
        [System.Serializable]
        class DerivedSerializationData
        {
            public bool repeat;
            public float updateInterval;
        }

        public override object OnDerivedDataSerialization() {
            var data = new DerivedSerializationData();
            data.repeat = this.repeat;
            data.updateInterval = this.updateInterval;
            return data;
        }

        public override void OnDerivedDataDeserialization(object data) {
            if ( data is DerivedSerializationData ) {
                this.repeat = ( (DerivedSerializationData)data ).repeat;
                this.updateInterval = ( (DerivedSerializationData)data ).updateInterval;
            }
        }
        ///----------------------------------------------------------------------------------------------

        ///Should the tree repeat forever?
        [System.NonSerialized] public bool repeat = true;
        ///The frequency in seconds for the tree to repeat if set to repeat.
        [System.NonSerialized] public float updateInterval = 0;

        ///Raised when the root status of the behaviour is changed
        public static event System.Action<BehaviourTree, Status> onRootStatusChanged;

        private float intervalCounter;
        private Status _rootStatus = Status.Resting;

        ///The last status of the root node
        public Status rootStatus {
            get { return _rootStatus; }
            private set
            {
                if ( _rootStatus != value ) {
                    _rootStatus = value;
                    if ( onRootStatusChanged != null ) {
                        onRootStatusChanged(this, value);
                    }
                }
            }
        }

        ///----------------------------------------------------------------------------------------------
        public override System.Type baseNodeType => typeof(BTNode);
        public override bool requiresAgent => true;
        public override bool requiresPrimeNode => true;
        public override bool isTree => true;
        public override bool allowBlackboardOverrides => true;
        sealed public override bool canAcceptVariableDrops => false;
        ///----------------------------------------------------------------------------------------------

        protected override void OnGraphStarted() {
            intervalCounter = updateInterval;
            rootStatus = primeNode.status;
        }

        protected override void OnGraphUpdate() {

            if ( intervalCounter >= updateInterval ) {
                intervalCounter = 0;
                if ( Tick(agent, blackboard) != Status.Running && !repeat ) {
                    Stop(rootStatus == Status.Success);
                }
            }

            if ( updateInterval > 0 ) {
                intervalCounter += Time.deltaTime;
            }
        }

        ///Tick the tree once for the provided agent and with the provided blackboard
        Status Tick(Component agent, IBlackboard blackboard) {
            if ( rootStatus != Status.Running ) { primeNode.Reset(); }
            return rootStatus = primeNode.Execute(agent, blackboard);
        }


        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        protected override UnityEditor.GenericMenu OnCanvasContextMenu(UnityEditor.GenericMenu menu, Vector2 canvasMousePos){
            menu.AddDisabledItem(new GUIContent("Composites/Binary Selector"));
            menu.AddDisabledItem(new GUIContent("Composites/Flip Selector"));
            menu.AddDisabledItem(new GUIContent("Composites/Priority Selector"));
            menu.AddDisabledItem(new GUIContent("Composites/Probability Selector"));
            menu.AddDisabledItem(new GUIContent("Composites/Step Sequencer"));
            menu.AddDisabledItem(new GUIContent("Composites/Switch"));

            menu.AddDisabledItem(new GUIContent("Decorators/Filter"));
            menu.AddDisabledItem(new GUIContent("Decorators/Guard"));
            menu.AddDisabledItem(new GUIContent("Decorators/Interrupt"));
            menu.AddDisabledItem(new GUIContent("Decorators/Iterate"));
            menu.AddDisabledItem(new GUIContent("Decorators/Monitor"));
            menu.AddDisabledItem(new GUIContent("Decorators/Optional"));
            menu.AddDisabledItem(new GUIContent("Decorators/Override"));
            menu.AddDisabledItem(new GUIContent("Decorators/Remap"));
            menu.AddDisabledItem(new GUIContent("Decorators/Repeat"));
            menu.AddDisabledItem(new GUIContent("Decorators/Timeout"));
            menu.AddDisabledItem(new GUIContent("Decorators/Wait Until"));

            menu.AddDisabledItem(new GUIContent("SubGraphs/SubTree"));
            menu.AddDisabledItem(new GUIContent("SubGraphs/SubFSM"));
            menu.AddDisabledItem(new GUIContent("SubGraphs/SubDialogue"));

            return menu;
        }

        [UnityEditor.MenuItem("Tools/ParadoxNotion/NodeCanvas/Create/Behaviour Tree Asset", false, 0)]
        static void Editor_CreateGraph() {
            var newGraph = EditorUtils.CreateAsset<BehaviourTree>();
            UnityEditor.Selection.activeObject = newGraph;
        }
#endif

    }
}