using System;
using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanCore.Factories
{
    public class KarassFactory : IKarassFactory
    {
        public Karass.Karass Get(Action setup, Action teardown, FrameRequest[] frames)
        {
            return new Karass.Karass(
                new List<List<Action>> {new List<Action> {setup}},
                new List<List<Action>> {new List<Action> {teardown}},
                new List<FrameRequest[]> {frames});
        }

        public Karass.Karass Get(List<Action> setup, List<Action> teardown, FrameRequest[] frames)
        {
            return new Karass.Karass(
                new List<List<Action>> {setup},
                new List<List<Action>> {teardown},
                new List<FrameRequest[]> {frames});
        }

        public Karass.Karass Get(List<List<Action>> setup, List<List<Action>> teardown, FrameRequest[] frames)
        {
            return new Karass.Karass(
                setup,
                teardown,
                new List<FrameRequest[]> {frames});
        }

        public Karass.Karass Get(Action setup, Action teardown, List<FrameRequest[]> frames)
        {
            return new Karass.Karass(
                new List<List<Action>> {new List<Action> {setup}},
                new List<List<Action>> {new List<Action> {teardown}},
                frames);
        }

        public Karass.Karass Get(List<Action> setup, List<Action> teardown, List<FrameRequest[]> frames)
        {
            return new Karass.Karass(
                new List<List<Action>> {setup},
                new List<List<Action>> {teardown},
                frames);
        }

        public Karass.Karass Get(List<List<Action>> setup, List<List<Action>> teardown,
            List<FrameRequest[]> frames)
        {
            return new Karass.Karass(
                setup,
                teardown,
                frames);
        }
    }
}