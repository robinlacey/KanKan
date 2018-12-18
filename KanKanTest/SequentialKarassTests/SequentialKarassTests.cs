using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.Dependencies;
using NUnit.Framework;

namespace KanKanTest.SequentialKarassTests
{
    public class SequentialKarassTests
    {
        public class GivenTwoEmptyKarass
        {
            [Test]
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

        public class GivenTwoFullKarassWithMatchingFrameCounts
        {
            [Test]
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
        
         public class GivenMultipleFullKarass
        {
            [Test]
            public void ThenKarassWillRunCorrectly()
            {
                KarassFactory karassFactory = new KarassFactory(new DependenciesDummy());

                bool karassOneSetupRun = false;

                void KarassOneSetupSpy()
                {
                    karassOneSetupRun = true;
                }

              

                bool karassOneTeardownRun = false;

                void KarassOneTeardownSpy()
                {
                    karassOneTeardownRun = true;
                }

              

                bool karassOneFrameOneRun = false;
                
                Karass karassOne = karassFactory.Get(KarassOneSetupSpy, KarassOneTeardownSpy,
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            _ => karassOneFrameOneRun = true
                        }
                    });


                bool karassTwoSetupRun = false;

                void KarassTwoSetupSpy()
                {
                    karassTwoSetupRun = true;
                }
                bool karassTwoTeardownRun = false;

                void KarassTwoTeardownSpy()
                {
                    karassTwoTeardownRun = true;
                }
                
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
                
                
                
                bool karassThreeSetupRun = false;

                void KarassThreeSetupSpy()
                {
                    karassThreeSetupRun = true;
                }
                bool karassThreeTeardownRun = false;

                void KarassThreeTeardownSpy()
                {
                    karassThreeTeardownRun = true;
                }
                
                bool karassThreeFrameOneRun = false;
                Karass karassThree = karassFactory.Get(KarassThreeSetupSpy, KarassThreeTeardownSpy,
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            _ => karassThreeFrameOneRun = true,
                        }
                    });
                
                
                
                
                bool karassFourSetupRun = false;

                void KarassFourSetupSpy()
                {
                    karassFourSetupRun = true;
                }
                bool karassFourTeardownRun = false;

                void KarassFourTeardownSpy()
                {
                    karassFourTeardownRun = true;
                }
                
                bool karassFourFrameOneRun = false;
                bool karassFourFrameTwoRun = false;
                Karass karassFour = karassFactory.Get(KarassFourSetupSpy, KarassFourTeardownSpy,
                    new List<Func<string, bool>[]>
                    {
                        new Func<string, bool>[]
                        {
                            _ => karassFourFrameOneRun = true,
                            _ => karassFourFrameTwoRun = true
                        }
                    });

                KanKan kankan = new KanKan(new IKarass[] {karassOne, karassTwo, karassThree ,karassFour}, new KarassMessage());
                bool moveNext = kankan.MoveNext();
                CheckFirstFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun, karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, moveNext, karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun, karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun);

                moveNext = kankan.MoveNext();
                
                
                CheckSecondFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun, karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun, karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);


                moveNext = kankan.MoveNext();
                
                CheckThirdFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun, karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun, karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);
                moveNext =  kankan.MoveNext();

                CheckFourthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun, karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun, karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);

                moveNext = kankan.MoveNext();

                CheckFifthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun, karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun, karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);

                moveNext = kankan.MoveNext();

                CheckSixthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun, karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun, karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);

                moveNext = kankan.MoveNext();
                
                CheckSeventhFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun, karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun, karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);
            }

            private static void CheckSeventhFrame(bool karassOneSetupRun, bool karassOneTeardownRun, bool karassOneFrameOneRun,
                bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun, bool karassThreeFrameOneRun,
                bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
                bool moveNext)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);

                Assert.True(karassTwoSetupRun);
                Assert.True(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.True(karassTwoFrameTwoRun);
                Assert.True(karassTwoFrameThreeRun);


                Assert.True(karassThreeSetupRun);
                Assert.True(karassThreeTeardownRun);
                Assert.True(karassThreeFrameOneRun);


                Assert.True(karassFourSetupRun);
                Assert.True(karassFourTeardownRun);
                Assert.True(karassFourFrameOneRun);
                Assert.True(karassFourFrameTwoRun);

                Assert.False(moveNext);
            }

            private static void CheckSixthFrame(bool karassOneSetupRun, bool karassOneTeardownRun, bool karassOneFrameOneRun,
                bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun, bool karassThreeFrameOneRun,
                bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
                bool moveNext)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);

                Assert.True(karassTwoSetupRun);
                Assert.True(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.True(karassTwoFrameTwoRun);
                Assert.True(karassTwoFrameThreeRun);


                Assert.True(karassThreeSetupRun);
                Assert.True(karassThreeTeardownRun);
                Assert.True(karassThreeFrameOneRun);


                Assert.True(karassFourSetupRun);
                Assert.False(karassFourTeardownRun);
                Assert.True(karassFourFrameOneRun);
                Assert.False(karassFourFrameTwoRun);

                Assert.True(moveNext);
            }

            private static void CheckFifthFrame(bool karassOneSetupRun, bool karassOneTeardownRun, bool karassOneFrameOneRun,
                bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun, bool karassThreeFrameOneRun,
                bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
                bool moveNext)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);

                Assert.True(karassTwoSetupRun);
                Assert.True(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.True(karassTwoFrameTwoRun);
                Assert.True(karassTwoFrameThreeRun);


                Assert.True(karassThreeSetupRun);
                Assert.True(karassThreeTeardownRun);
                Assert.True(karassThreeFrameOneRun);


                Assert.False(karassFourSetupRun);
                Assert.False(karassFourTeardownRun);
                Assert.False(karassFourFrameOneRun);
                Assert.False(karassFourFrameTwoRun);

                Assert.True(moveNext);
            }

            private static void CheckFourthFrame(bool karassOneSetupRun, bool karassOneTeardownRun, bool karassOneFrameOneRun,
                bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun, bool karassThreeFrameOneRun,
                bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
                bool moveNext)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);

                Assert.True(karassTwoSetupRun);
                Assert.True(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.True(karassTwoFrameTwoRun);
                Assert.True(karassTwoFrameThreeRun);


                Assert.False(karassThreeSetupRun);
                Assert.False(karassThreeTeardownRun);
                Assert.False(karassThreeFrameOneRun);


                Assert.False(karassFourSetupRun);
                Assert.False(karassFourTeardownRun);
                Assert.False(karassFourFrameOneRun);
                Assert.False(karassFourFrameTwoRun);

                Assert.True(moveNext);
            }

            private static void CheckThirdFrame(bool karassOneSetupRun, bool karassOneTeardownRun, bool karassOneFrameOneRun,
                bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun, bool karassThreeFrameOneRun,
                bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
                bool moveNext)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);

                Assert.True(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.True(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);


                Assert.False(karassThreeSetupRun);
                Assert.False(karassThreeTeardownRun);
                Assert.False(karassThreeFrameOneRun);


                Assert.False(karassFourSetupRun);
                Assert.False(karassFourTeardownRun);
                Assert.False(karassFourFrameOneRun);
                Assert.False(karassFourFrameTwoRun);

                Assert.True(moveNext);
            }

            private static void CheckSecondFrame(bool karassOneSetupRun, bool karassOneTeardownRun, bool karassOneFrameOneRun,
                bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun, bool karassThreeFrameOneRun,
                bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
                bool moveNext)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);

                Assert.True(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
                Assert.True(karassTwoFrameOneRun);
                Assert.False(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);


                Assert.False(karassThreeSetupRun);
                Assert.False(karassThreeTeardownRun);
                Assert.False(karassThreeFrameOneRun);


                Assert.False(karassFourSetupRun);
                Assert.False(karassFourTeardownRun);
                Assert.False(karassFourFrameOneRun);
                Assert.False(karassFourFrameTwoRun);

                Assert.True(moveNext);
            }

            private static void CheckFirstFrame(bool karassOneSetupRun, bool karassOneTeardownRun, bool karassOneFrameOneRun,
                bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
                bool karassTwoFrameThreeRun, bool moveNext, bool karassThreeSetupRun, bool karassThreeTeardownRun,
                bool karassThreeFrameOneRun, bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun,
                bool karassFourFrameTwoRun)
            {
                Assert.True(karassOneSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassOneFrameOneRun);

                Assert.False(karassTwoSetupRun);
                Assert.False(karassTwoTeardownRun);
                Assert.False(karassTwoFrameOneRun);
                Assert.False(karassTwoFrameTwoRun);
                Assert.False(karassTwoFrameThreeRun);

                Assert.True(moveNext);

                Assert.False(karassThreeSetupRun);
                Assert.False(karassThreeTeardownRun);
                Assert.False(karassThreeFrameOneRun);


                Assert.False(karassFourSetupRun);
                Assert.False(karassFourTeardownRun);
                Assert.False(karassFourFrameOneRun);
                Assert.False(karassFourFrameTwoRun);
            }
        }
    }
}