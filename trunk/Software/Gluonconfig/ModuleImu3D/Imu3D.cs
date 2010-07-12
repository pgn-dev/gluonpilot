#region BSD License
/*
 BSD License
Copyright (c) 2002, Randy Ridge, The CsGL Development Team
http://csgl.sourceforge.net/
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright notice,
   this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of The CsGL Development Team nor the names of its
   contributors may be used to endorse or promote products derived from this
   software without specific prior written permission.

   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
   FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
   COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
   INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
   BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
   LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
   CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
   LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
   ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
   POSSIBILITY OF SUCH DAMAGE.
 */
#endregion BSD License

#region Original Credits / License
/*
 *		This Code Was Created By Jeff Molofee 2000
 *		A HUGE Thanks To Fredric Echols For Cleaning Up
 *		And Optimizing The Base Code, Making It More Flexible!
 *		If You've Found This Code Useful, Please Let Me Know.
 *		Visit My Site At nehe.gamedev.net
 */
#endregion Original Credits / License

using CsGL.Basecode;
using System.Reflection;
using Communication;


namespace ModuleImu3D
{
    /// <summary>
    /// NeHe Lesson 05 -- 3D Shapes (http://nehe.gamedev.net)
    /// Implemented In C# By The CsGL Development Team (http://csgl.sourceforge.net)
    /// </summary>
    public sealed class Imu3D : Model
    {
        // --- Fields ---
        #region Private Fields
        private static float rtri = 0;													// Angle For The Triangle
        private static float rquad = 0;													// Angle For The Quad
        private SerialCommunication serial;
        private float roll = 0.0f, pitch = 0.0f, yaw = 0.0f;
        #endregion Private Fields

        public Imu3D(SerialCommunication serial)
        {
            this.serial = serial;
            serial.AttitudeCommunicationReceived += new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
        }

        void serial_AttitudeCommunicationReceived(Communication.Frames.Incoming.Attitude attitude)
        {
            roll = (float)attitude.RollDeg;
            pitch = (float)attitude.PitchDeg;
            yaw = -(float)attitude.YawDeg;
        }

        #region Public Properties
        /// <summary>
        /// Lesson title.
        /// </summary>
        public override string Title
        {
            get
            {
                return "Gluonpilot - IMU module attitude 3D representation - Purple/Right is front, Green is top";
            }
        }

        /// <summary>
        /// Lesson description.
        /// </summary>
        public override string Description
        {
            get
            {
                return "See http://www.gluonpilot.com";
            }
        }

        /// <summary>
        /// Lesson URL.
        /// </summary>
        public override string Url
        {
            get
            {
                return "http://www.gluonpilot.com";
            }
        }
        #endregion Public Properties

        
        public static void Run(SerialCommunication serial)
        {														// Entry Point
            Imu3D n = new Imu3D(serial);
            App.Run(n);												// Run Our NeHe Lesson As A Windows Forms Application

        }

        // --- Basecode Methods ---
        #region Draw()
        /// <summary>
        /// Draws NeHe Lesson 05 scene.
        /// </summary>
        public override void Draw()
        {													// Here's Where We Do All The Drawing
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);							// Clear Screen And Depth Buffer
            
            glLoadIdentity();															// Reset The Current Modelview Matrix
            glTranslatef(1.5f, 0.0f, -7.0f);											// Move Right 1.5 Units And Into The Screen 7.0

            glRotatef(yaw, 0.0f, 1.0f, 0.0f);
            glRotatef(pitch, 0.0f, 0.0f, 1.0f);
            glRotatef(roll, 1.0f, 0.0f, 0.0f);
            
            
            glBegin(GL_QUADS);															// Draw A Quad
            glColor3f(0.0f, 1.0f, 0.0f);											// Set The Color To Green
            glVertex3f(1.0f, 0.5f, -1.0f);											// Top Right Of The Quad (Top)
            glVertex3f(-2.0f, 0.5f, -1.0f);											// Top Left Of The Quad (Top)
            glVertex3f(-2.0f, 0.5f, 1.0f);											// Bottom Left Of The Quad (Top)
            glVertex3f(1.0f, 0.5f, 1.0f);											// Bottom Right Of The Quad (Top)

            glColor3f(1.0f, 0.5f, 0.0f);										// Set The Color To Orange
            glVertex3f(1.0f, -0.5f, 1.0f);										// Top Right Of The Quad (Bottom)
            glVertex3f(-2.0f, -0.5f, 1.0f);										// Top Left Of The Quad (Bottom)
            glVertex3f(-2.0f, -0.5f, -1.0f);										// Bottom Left Of The Quad (Bottom)
            glVertex3f(1.0f, -0.5f, -1.0f);										// Bottom Right Of The Quad (Bottom)

            glColor3f(1.0f, 0.0f, 0.0f);											// Set The Color To Red
            glVertex3f(1.0f, 0.5f, 1.0f);											// Top Right Of The Quad (Front)
            glVertex3f(-2.0f, 0.5f, 1.0f);											// Top Left Of The Quad (Front)
            glVertex3f(-2.0f, -0.5f, 1.0f);											// Bottom Left Of The Quad (Front)
            glVertex3f(1.0f, -0.5f, 1.0f);											// Bottom Right Of The Quad (Front)

            glColor3f(1.0f, 1.0f, 0.0f);										// Set The Color To Yellow
            glVertex3f(1.0f, -0.5f, -1.0f);										// Bottom Left Of The Quad (Back)
            glVertex3f(-2.0f, -0.5f, -1.0f);										// Bottom Right Of The Quad (Back)
            glVertex3f(-2.0f, 0.5f, -1.0f);										// Top Right Of The Quad (Back)
            glVertex3f(1.0f, 0.5f, -1.0f);										// Top Left Of The Quad (Back)

            glColor3f(0.0f, 0.0f, 1.0f);										// Set The Color To Blue
            glVertex3f(-2.0f, 0.5f, 1.0f);										// Top Right Of The Quad (Left)
            glVertex3f(-2.0f, 0.5f, -1.0f);										// Top Left Of The Quad (Left)
            glVertex3f(-2.0f, -0.5f, -1.0f);										// Bottom Left Of The Quad (Left)
            glVertex3f(-2.0f, -0.5f, 1.0f);										// Bottom Right Of The Quad (Left)

            glColor3f(1.0f, 0.0f, 1.0f);											// Set The Color To Violet
            glVertex3f(1.0f, 0.5f, -1.0f);											// Top Right Of The Quad (Right)
            glVertex3f(1.0f, 0.5f, 1.0f);											// Top Left Of The Quad (Right)
            glVertex3f(1.0f, -0.5f, 1.0f);											// Bottom Left Of The Quad (Right)
            glVertex3f(1.0f, -0.5f, -1.0f);											// Bottom Right Of The Quad (Right)
            glEnd();																	// Done Drawing The Cube

            //rtri += 0.2f;																// Increase The Rotation Variable For The Triangle
            //rquad -= 0.15f;																// Decrease The Rotation Variable For The Quad
        }
        #endregion Draw()
    }
}
