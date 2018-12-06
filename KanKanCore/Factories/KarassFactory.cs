using System;
using System.Collections.Generic;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Factories
{
    public class KarassFactory : IKarassFactory
    {
        private readonly IDependencies _dependencies;

        public KarassFactory(IDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public Karass.Karass Get(Action setup, Action teardown, Func<string, bool>[] frames)
        {
            return new Karass.Karass(
                _dependencies,
                new List<List<Action>> {new List<Action> {setup}},
                new List<List<Action>> {new List<Action> {teardown}},
                new List<Func<string, bool>[]> {frames});
        }

        public Karass.Karass Get(List<Action> setup, List<Action> teardown, Func<string, bool>[] frames)
        {
            return new Karass.Karass(
                _dependencies,
                new List<List<Action>> {setup},
                new List<List<Action>> {teardown},
                new List<Func<string, bool>[]> {frames});
        }

        public Karass.Karass Get(List<List<Action>> setup, List<List<Action>> teardown, Func<string, bool>[] frames)
        {
            return new Karass.Karass(
                _dependencies,
                setup,
                teardown,
                new List<Func<string, bool>[]> {frames});
        }

        public Karass.Karass Get(Action setup, Action teardown, List<Func<string, bool>[]> frames)
        {
            return new Karass.Karass(
                _dependencies,
                new List<List<Action>> {new List<Action> {setup}},
                new List<List<Action>> {new List<Action> {teardown}},
                frames);
        }

        public Karass.Karass Get(List<Action> setup, List<Action> teardown, List<Func<string, bool>[]> frames)
        {
            return new Karass.Karass(
                _dependencies,
                new List<List<Action>> {setup},
                new List<List<Action>> {teardown},
                frames);
        }

        public Karass.Karass Get(List<List<Action>> setup, List<List<Action>> teardown,
            List<Func<string, bool>[]> frames)
        {
            return new Karass.Karass(
                _dependencies,
                setup,
                teardown,
                frames);
        }
    }
}