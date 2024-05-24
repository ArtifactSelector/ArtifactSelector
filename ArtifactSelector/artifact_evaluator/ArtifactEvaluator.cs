using ArtifactSelector.model;
using System;
using System.Collections.Generic;

namespace ArtifactSelector.artifact_evaluator
{
    enum ArtifactAction
    {
        Keep,
        Trash,
        Lock,
        None
    }

    internal class ArtifactEvaluator
    {
        private Func<Artifact, ArtifactAction> evaluator;

        public ArtifactEvaluator(ArtifactAction action, BooleanEvaluator booleanEvaluator)
        {
            this.evaluator = artifact =>
            {
                if (booleanEvaluator.Evaluate(artifact))
                {
                    return action;
                }
                else
                {
                    return ArtifactAction.None;
                }
            };
        }

        public ArtifactEvaluator(ArtifactAction action, List<ArtifactEvaluator> evaluators)
        {
            this.evaluator = artifact =>
            {
                foreach (ArtifactEvaluator eval in evaluators)
                {
                    if (eval.Evaluate(artifact) != ArtifactAction.None)
                    {
                        return eval.Evaluate(artifact);
                    }
                }

                return action;
            };
        }

        public ArtifactEvaluator(BooleanEvaluator booleanEvaluator, ArtifactEvaluator artifactEvaluator)
        {
            this.evaluator = artifact =>
            {
                if (booleanEvaluator.Evaluate(artifact))
                {
                    return artifactEvaluator.Evaluate(artifact);
                }
                else
                {
                    return ArtifactAction.None;
                }
            };
        }

        public ArtifactAction Evaluate(Artifact artifact)
        {
            return evaluator.Invoke(artifact);
        }
    }
}
