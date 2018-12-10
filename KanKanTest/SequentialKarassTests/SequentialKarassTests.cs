using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.Dependencies;
using Xunit;

namespace KanKanTest.SequentialKarassTests
{
    public class SequentialKarassTests
    {
        public class GivenTwoEmptyKarass
        {
            [Fact]
            public void BothKarassSetupAndTeardownWillBeRun()
            {
                KarassFactory karassFactory = new KarassFactory(new DependenciesDummy());

                bool karassOneSetupRun = false;

                void KarassOneSetupSpy()
                {
                    karassOneSetupRun = true;
                }

                bool karassTwoSetupRun = false;

                void KarassTwoSetupSpy()
                {
                    karassTwoSetupRun = true;
                }

                bool karassOneTeardownRun = false;

                void KarassOneTeardownSpy()
                {
                    karassOneTeardownRun = true;
                }

                bool karassTwoTeardownRun = false;

                void KarassTwoTeardownSpy()
                {
                    karassTwoTeardownRun = true;
                }

                Karass karassOne = karassFactory.Get(KarassOneSetupSpy, KarassOneTeardownSpy,
                    new List<Func<string, bool>[]>());

                Karass karassTwo = karassFactory.Get(KarassTwoSetupSpy, KarassTwoTeardownSpy,
                    new List<Func<string, bool>[]>());

                KanKan kankan = new KanKan(new IKarass[] {karassOne, karassTwo}, new KarassMessage());

                kankan.MoveNext();

                Assert.True(karassOneSetupRun);
                Assert.True(karassTwoSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassTwoTeardownRun);
            }
        }

        public class GivenTwoFullKarass
        {
            [Fact]
            public void BothKarassWillBeRun()
            {
                KarassFactory karassFactory = new KarassFactory(new DependenciesDummy());

                bool karassOneSetupRun = false;

                void KarassOneSetupSpy()
                {
                    karassOneSetupRun = true;
                }

                bool karassTwoSetupRun = false;

                void KarassTwoSetupSpy()
                {
                    karassTwoSetupRun = true;
                }

                bool karassOneTeardownRun = false;

                void KarassOneTeardownSpy()
                {
                    karassOneTeardownRun = true;
                }

                bool karassTwoTeardownRun = false;

                void KarassTwoTeardownSpy()
                {
                    karassTwoTeardownRun = true;
                }

                bool karassOneFrameOneRun = false;
                bool karassOneFrameTwoRun = false;
                bool karassOneFrameThreeRun = false;
                Karass karassOne = karassFactory.Get(KarassOneSetupSpy, KarassOneTeardownSpy,
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            _ => karassOneFrameOneRun = true,
                            _ => karassOneFrameTwoRun = true,
                            _ => karassOneFrameThreeRun = true
                        }
                    });


                bool karassTwoFrameOneRun = false;
                bool karassTwoFrameTwoRun = false;
                bool karassTwoFrameThreeRun = false;
                Karass karassTwo = karassFactory.Get(KarassTwoSetupSpy, KarassTwoTeardownSpy,
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            _ => karassTwoFrameOneRun = true,
                            _ => karassTwoFrameTwoRun = true,
                            _ => karassTwoFrameThreeRun = true
                        }
                    });

                KanKan kankan = new KanKan(new IKarass[] {karassOne, karassTwo}, new KarassMessage());

                kankan.MoveNext();
                CheckFirstFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                    karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                    karassTwoSetupRun, karassTwoTeardownRun);

                kankan.MoveNext();
                CheckSecondFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                    karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                    karassTwoSetupRun, karassTwoTeardownRun);

                kankan.MoveNext();
                CheckThirdFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                    karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                    karassTwoSetupRun, karassTwoTeardownRun);


                kankan.MoveNext();
                CheckFourthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                    karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                    karassTwoSetupRun, karassTwoTeardownRun);

                kankan.MoveNext();
                CheckFifthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                    karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                    karassTwoSetupRun, karassTwoTeardownRun);

                kankan.MoveNext();
                CheckSixthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                    karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                    karassTwoSetupRun, karassTwoTeardownRun);
            }

            private static void CheckFirstFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
                bool karassOneFrameOneRun,
                bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
                bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
            {
                Assert.True(karassOneSetupRun);
                Assert.False(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);
                Assert.False(karassOneFrameTwoRun);
                Assert.False(karassOneFrameThreeRun);

                Assert.False(karassTwoFrameOneRun);
                Assert.False(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);

                Assert.False(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
            }

            private static void CheckSecondFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
                bool karassOneFrameOneRun,
                bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
                bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
            {
                Assert.True(karassOneSetupRun);
                Assert.False(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);
                Assert.True(karassOneFrameTwoRun);
                Assert.False(karassOneFrameThreeRun);

                Assert.False(karassTwoFrameOneRun);
                Assert.False(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);

                Assert.False(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
            }

            private static void CheckThirdFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
                bool karassOneFrameOneRun,
                bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
                bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);
                Assert.True(karassOneFrameTwoRun);
                Assert.True(karassOneFrameThreeRun);

                Assert.False(karassTwoFrameOneRun);
                Assert.False(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);

                Assert.False(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
            }

            private static void CheckFourthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
                bool karassOneFrameOneRun,
                bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
                bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);
                Assert.True(karassOneFrameTwoRun);
                Assert.True(karassOneFrameThreeRun);

                Assert.True(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.False(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);
            }

            private static void CheckFifthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
                bool karassOneFrameOneRun,
                bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
                bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);
                Assert.True(karassOneFrameTwoRun);
                Assert.True(karassOneFrameThreeRun);

                Assert.True(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.True(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);
            }

            private static void CheckSixthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
                bool karassOneFrameOneRun,
                bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
                bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);
                Assert.True(karassOneFrameTwoRun);
                Assert.True(karassOneFrameThreeRun);

                Assert.True(karassTwoSetupRun);
                Assert.True(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.True(karassTwoFrameTwoRun);
                Assert.True(karassTwoFrameThreeRun);
            }
        }
    }
}