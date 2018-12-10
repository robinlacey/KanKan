using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Interface;

namespace KanKanCore
{
    public class KanKan : IEnumerator
    {
        public object Current => CurrentData.NextFrames;
        private readonly IKarassMessage _message;
        private int _currentKarass;
        public KarassData CurrentData => _allKarassData[_currentKarass];
        private List<KarassData> _allKarassData;


        public KanKan(IKarass karass, IKarassMessage karassMessage)
        {
            _allKarassData = new List<KarassData> {new KarassData(karass)};
            _message = karassMessage;
        }

        public KanKan(IKarass[] karass, IKarassMessage karassMessage)
        {
            _allKarassData = karass.ToList().Select(_ => new KarassData(_)).ToList();

            for (int i = 0; i < karass.Length; i++)
            {
                _allKarassData[i] = new KarassData(karass[i]);
            }

            _message = karassMessage;
        }

        public void SendMessage(string message)
        {
            _message.SetMessage(message);
        }


        public bool MoveNext()
        {
            KarassData currentKarass = _allKarassData[_currentKarass];

            int lastFrameCount = 0;

            if (currentKarass.EmptyKarass())
            {
                currentKarass.InvokeAllSetupActions();
                currentKarass.InvokeAllTeardownActions();

                if (_allKarassData.Count - 1 >= _currentKarass + 1)
                {
                    _currentKarass++;
                    return MoveNext();
                }

                return false;
            }

            currentKarass.NextFrames.Clear();

            for (int i = 0; i < currentKarass.Karass.FramesCollection.Count; i++)
            {
                if (currentKarass.FrameSetAlreadyFinished(i))
                {
                    continue;
                }

                currentKarass.InvokeSetupActionsOnFirstFrame(
                    currentKarass.CurrentFrames[currentKarass.Karass.FramesCollection[i]], i);

                if (!currentKarass.InvokeCurrentFrame(i,
                    currentKarass.CurrentFrames[currentKarass.Karass.FramesCollection[i]], _message)) continue;

                currentKarass.InvokeTeardownActionsIfLastFrame(i,
                    currentKarass.AddFrame(currentKarass.Karass.FramesCollection[i]),
                    currentKarass.Karass.FramesCollection[i],
                    ref lastFrameCount,
                    out bool shouldComplete);

                if (shouldComplete)
                {
                    // _currentKarass ++ 
                    // if no more then false
                    if (_allKarassData.Count - 1 >= _currentKarass + 1)
                    {
                        _currentKarass++;
                        return true;
                    }

                    return false;
                }
            }

            _message.ClearMessage();

            return true;
        }


        public void Reset()
        {
            for (int i = 0; i < _allKarassData.Count; i++)
            {
                _allKarassData[i].Reset();
            }
        }
    }
}