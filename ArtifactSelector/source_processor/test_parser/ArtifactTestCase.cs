using ArtifactSelector.artifact_evaluator;
using ArtifactSelector.model;

namespace ArtifactSelector.source_processor.test_parser
{
    internal class ArtifactTestCase
    {
        private readonly Artifact artifact;
        private readonly ArtifactAction action;
        private readonly string name;

        public ArtifactTestCase(Artifact artifact, ArtifactAction action, string name)
        {
            this.artifact = artifact;
            this.action = action;
            this.name = name;
        }

        public Artifact Artifact { get { return artifact; } }
        public ArtifactAction Action { get { return action; } }
        public string Name { get { return name; } }
    }
}
