using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Factories
{
    public class KarassFactory : IKarassFactory
    {
        private readonly IDependencies _dependencies;
        private readonly IFrameFactory _frameFactory;
        public KarassFactory(IDependencies dependencies, IFrameFactory frameFactory)
        {
            _frameFactory = frameFactory;
            _dependencies = dependencies;
        }

        public Karass.Karass Get(Action setup, Action teardown, FrameRequest[] frames)
        {
            return new Karass.Karass(
                _dependencies,
                _frameFactory,
                new List<List<Action>> {new List<Action> {setup}},
                new List<List<Action>> {new List<Action> {teardown}},
                new List<FrameRequest[]> {frames});
        }

        public Karass.Karass Get(List<Action> setup, List<Action> teardown, FrameRequest[] frames)
        {
            return new Karass.Karass(
                _dependencies,
                _frameFactory,
                new List<List<Action>> {setup},
                new List<List<Action>> {teardown},
                new List<FrameRequest[]> {frames});
        }

        public Karass.Karass Get(List<List<Action>> setup, List<List<Action>> teardown, FrameRequest[] frames)
        {
            return new Karass.Karass(
                _dependencies,
                _frameFactory,
                setup,
                teardown,
                new List<FrameRequest[]> {frames});
        }

        public Karass.Karass Get(Action setup, Action teardown, List<FrameRequest[]> frames)
        {
            return new Karass.Karass(
                _dependencies,
                _frameFactory,
                new List<List<Action>> {new List<Action> {setup}},
                new List<List<Action>> {new List<Action> {teardown}},
                frames);
        }

        public Karass.Karass Get(List<Action> setup, List<Action> teardown, List<FrameRequest[]> frames)
        {
            return new Karass.Karass(
                _dependencies,
                _frameFactory,
                new List<List<Action>> {setup},
                new List<List<Action>> {teardown},
                frames);
        }

        public Karass.Karass Get(List<List<Action>> setup, List<List<Action>> teardown,
            List<FrameRequest[]> frames)
        {
            return new Karass.Karass(
                _dependencies,
                _frameFactory,
                setup,
                teardown,
                frames);
        }
    }
}