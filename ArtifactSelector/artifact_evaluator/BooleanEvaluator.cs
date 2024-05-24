using ArtifactSelector.model;
using System;

namespace ArtifactSelector.artifact_evaluator
{
    public class BooleanEvaluator
    {
        private Func<Artifact, bool> evaluator;

        public BooleanEvaluator(Func<Artifact, bool> evaluator)
        {
            this.evaluator = evaluator;
        }

        public BooleanEvaluator And(BooleanEvaluator other)
        {
            return new BooleanEvaluator(artifact => Evaluate(artifact) && other.Evaluate(artifact));
        }

        public BooleanEvaluator Or(BooleanEvaluator other)
        {
            return new BooleanEvaluator(artifact => Evaluate(artifact) || other.Evaluate(artifact));
        }

        public BooleanEvaluator Negate()
        {
            return new BooleanEvaluator(artifact => !Evaluate(artifact));
        }

        public bool Evaluate(Artifact artifact)
        {
            return evaluator.Invoke(artifact);
        }
    }
}
