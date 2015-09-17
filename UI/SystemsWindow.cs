using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.UI
{
    class SystemsWindow : Window
    {
        Text engineText;
        Text thrustText;
        Text thrustMaxText;
        ProgressBar thrustbar;

        public SystemsWindow()
            : base(2, 1, 5)
        {
            FrameComponent frame, frame1;
            ProgressBar bar;
            Text text;
            Window window;
            Text title = new Text("Ship Systems Overview", true);
            title.centerX = false;
            set(0, 0, title);

            FrameComponent subFrame = new FrameComponent(1, 2);
            set(1, 0, subFrame);

            #region LEFT
            FrameComponent left = new FrameComponent(5, 1, 5);
            left.centerY = false;
            subFrame.set(0, 0, left);
            {
                #region POWER
                frame = new FrameComponent(2, 1, 5);
                frame.fill = true;
                left.set(0, 0, frame);
                {
                    title = new Text("Power", true);
                    title.centerX = false;
                    frame.set(0, 0, title);

                    window = new Window(2, 2, 5);
                    window.color_ = Color.Black;
                    window.fill = true;
                    frame.set(1, 0, window);
                    {
                        text = new Text("Demand/Supply:", false, 5);
                        text.centerX = false;
                        window.set(0, 0, text);

                        frame1 = new FrameComponent(1, 3, 5);
                        window.set(0, 1, frame1);
                        {
                            text = new Text("4.0 GW", false, 5);
                            frame1.set(0, 0, text);

                            bar = new ProgressBar();
                            bar.percent = 4f / 6.8f;
                            frame1.set(0, 1, bar);

                            text = new Text("6.8 GW", false, 5);
                            frame1.set(0, 2, text);
                        }

                        text = new Text("Reserves:", false, 5);
                        text.centerX = false;
                        window.set(1, 0, text);

                        frame1 = new FrameComponent(1, 3, 5);
                        window.set(1, 1, frame1);
                        {
                            text = new Text("0.0 J ", false, 5);
                            frame1.set(0, 0, text);

                            bar = new ProgressBar();
                            bar.percent = 0;
                            frame1.set(0, 1, bar);

                            text = new Text("0.0 J ", false, 5);
                            frame1.set(0, 2, text);
                        }
                    }
                }
                #endregion
                #region SHIELD
                frame = new FrameComponent(2, 1, 5);
                frame.fill = true;
                left.set(1, 0, frame);
                {
                    title = new Text("Shields", true);
                    title.centerX = false;
                    frame.set(0, 0, title);

                    window = new Window(1, 3, 5);
                    window.color_ = Color.Black;
                    window.fill = true;
                    frame.set(1, 0, window);
                    {
                        text = new Text("0 P", false, 5);
                        window.set(0, 0, text);

                        bar = new ProgressBar();
                        bar.fg = Color.Red;
                        bar.percent = 1;
                        window.set(0, 1, bar);

                        text = new Text("0 P", false, 5);
                        window.set(0, 2, text);
                    }
                }
                #endregion
                #region ENGINES
                frame = new FrameComponent(2, 1, 5);
                frame.fill = true;
                left.set(2, 0, frame);
                {
                    title = new Text("Engines", true);
                    title.centerX = false;
                    frame.set(0, 0, title);

                    window = new Window(2, 2, 5);
                    window.color_ = Color.Black;
                    window.fill = true;
                    frame.set(1, 0, window);
                    {
                        text = new Text("Engines Online:", false, 5);
                        text.centerX = false;
                        window.set(0, 0, text);

                        engineText = new Text("0 / 6", false, 5);
                        window.set(0, 1, engineText);

                        text = new Text("Thrust:", false, 5);
                        text.centerX = false;
                        window.set(1, 0, text);

                        frame1 = new FrameComponent(1, 3, 5);
                        window.set(1, 1, frame1);
                        {
                            thrustText = new Text("0.0 N ", false, 5);
                            frame1.set(0, 0, thrustText);

                            thrustbar = new ProgressBar();
                            frame1.set(0, 1, thrustbar);

                            thrustMaxText = new Text("47.3 MN", false, 5);
                            frame1.set(0, 2, thrustMaxText);
                        }
                    }
                }
                #endregion
                #region WEAPONS
                frame = new FrameComponent(2, 1, 5);
                frame.fill = true;
                left.set(3, 0, frame);
                {
                    title = new Text("Weapons", true);
                    title.centerX = false;
                    frame.set(0, 0, title);

                    window = new Window(3, 2, 5);
                    window.color_ = Color.Black;
                    window.fill = true;
                    frame.set(1, 0, window);
                    {
                        text = new Text("Kinetic Turrets Online:", false, 5);
                        text.centerX = false;
                        window.set(0, 0, text);

                        text = new Text("0 / 0", false, 5);
                        window.set(0, 1, text);

                        text = new Text("Laser Turrets Online:", false, 5);
                        text.centerX = false;
                        window.set(1, 0, text);

                        text = new Text("1 / 2", false, 5);
                        window.set(1, 1, text);


                        text = new Text("Missile Turrets Online:", false, 5);
                        text.centerX = false;
                        window.set(2, 0, text);

                        text = new Text("0 / 0", false, 5);
                        window.set(2, 1, text);
                    }
                }
                #endregion
                #region SENSORS
                frame = new FrameComponent(2, 1);
                frame.fill = true;
                left.set(4, 0, frame);
                {
                    title = new Text("Sensors", true);
                    title.centerX = false;
                    frame.set(0, 0, title);

                    window = new Window(1, 2, 5);
                    window.color_ = Color.Black;
                    window.fill = true;
                    frame.set(1, 0, window);
                    {
                        text = new Text("Effective Range:", false, 5);
                        text.centerX = false;
                        window.set(0, 0, text);

                        text = new Text("384.4 Mm", false, 5);
                        window.set(0, 1, text);
                    }
                }
                #endregion
            }
            #endregion
            #region RIGHT
            FrameComponent right = new FrameComponent(1, 1, 5);
            subFrame.set(0, 1, right);
            {
                #region MAP
                Map map = new Map();
                map.padding_ = new Vector2(10);
                right.set(0, 0, map);
                #endregion
            }
            #endregion

            pack();
            Locator.getMessageBoard().register(onPost);
        }

        public void onPost(Services.Post post)
        {
            switch (post.category)
            {
                case Services.PostCategory.END_GAME:
                    thrustText.text_ = "7.9 MN";
                    thrustbar.percent = 7.9f / 39.4f;
                    break;
                case Services.PostCategory.REPAIRED_ENGINE:
                    engineText.text_ = "1 / 5";
                    thrustMaxText.text_ = "39.4 MN";
                    break;
                default:
                    break;
            }
        }
    }
}
