// ATT GÖRA
// NY FÄRG (edc7ff)
// RECTANGLEREC COLLISION INKL KEYS
// DIAGONELL COLISSION CHECK BEROENDE PÅ VAR SPELAREN ÄR PÅVÄG MED PLAYERSPEED
// BANA 2
// TEXTUR PÅ NYCKLARNA
// BÄTTRE DESIGN PÅ DEN LÅSTA DÖRREN KANSKE OCKSÅ TEXTUR

using System;
using Raylib_cs;
using System.Numerics;

namespace graphics
{
    class Program
    {
        static void Main(string[] args)
        {
            const int screenWidth = 1920;
            const int screenHeight = 1000;
            Raylib.InitWindow(screenWidth, screenHeight, "GAME");

            Texture2D thirdBackground = Raylib.LoadTexture("thirdbackground.png");
            Texture2D secondBackground = Raylib.LoadTexture("secondbackground.png");
            Texture2D firstBackground = Raylib.LoadTexture("firstbackground.png");

            //BACKGROUND SCROLL VALUES
            float scrollThird = 0.0f;
            float scrollSecond = 0.0f;
            float scrollFirst = 0.0f;

            //PLAYER VALUES
            float playerX = 225;
            float playerY = 225;
            string direction = "";

            //GAME VALUES
            int deaths = 0;
            int keys = 0;

            int keyOneX = 825;
            int keyOneY = 425;
            int keyTwoX = 1225;
            int keyTwoY = 625;

            //ENEMY VALUES
            float enemyX = 650;
            float enemyY = 225;
            float enemySpeed = 1.2f;

            //LEVEL, STATE AND GOAL VALUES
            string level = "one";
            string gameState = "intro";
            bool completed = false;

            //MENU COLOR VALUES
            int menuTarget = 1;
            Color menuResumeColor = Color.GRAY;
            Color menuOptionsColor = Color.GRAY;
            Color menuExitColor = Color.GRAY;

            //PLAYER COLOR VALUES
            Color playerColor = Color.PURPLE;
            Color[] playerColors = { Color.RED, Color.ORANGE, Color.YELLOW, Color.GREEN, Color.BLUE, Color.PURPLE, Color.PINK, Color.WHITE };
            int playerArrayIndex = 0;

            Raylib.SetTargetFPS(450); //KONTROLLERA FPS FÖR ATT SPELARENS HASTIGHET ÄR BEROENDE AV DEN

            while (!Raylib.WindowShouldClose())
            {
                playerColor = playerColors[playerArrayIndex];
                //INTRO LOOP
                if (gameState == "intro")
                {
                    //GE VÄRDEN PÅ VARIABLER, OLIKA HASTIGHETER FÖR DJUP
                    scrollFirst -= 0.3f;
                    scrollSecond -= 0.2f;
                    scrollThird -= 0.1f;

                    //SKICKA TILLBAKA BILDERNA NÄR DE ÅKER FÖR LÅNGT ÅT VÄNSTER
                    if (scrollFirst <= -firstBackground.width * 2)
                    {
                        scrollFirst = 0;
                    }
                    if (scrollSecond <= -secondBackground.width * 2)
                    {
                        scrollSecond = 0;
                    }
                    if (scrollThird <= -thirdBackground.width * 2)
                    {
                        scrollThird = 0;
                    }

                    //BÖRJA RAYLIB
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);

                    //RITA UT TEXTURERNA TVÅ GÅNGER BREDVID VARANDRA OCH FLYTTA BÅDA ÅT VÄNSTER
                    Raylib.DrawTextureEx(thirdBackground, new Vector2(scrollThird, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(thirdBackground, new Vector2(thirdBackground.width * 2 + scrollThird, 0), 0.0f, 2.0f, Color.WHITE);

                    Raylib.DrawTextureEx(secondBackground, new Vector2(scrollSecond, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(secondBackground, new Vector2(secondBackground.width * 2 + scrollSecond, 0), 0.0f, 2.0f, Color.WHITE);

                    Raylib.DrawTextureEx(firstBackground, new Vector2(scrollFirst, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(firstBackground, new Vector2(firstBackground.width * 2 + scrollFirst, 0), 0.0f, 2.0f, Color.WHITE);

                    Raylib.DrawText("THE COLORED SQUARE GAME", 200, 200, 32, Color.BLACK);
                    Raylib.DrawText("COLORED", 281, 200, 32, Color.PURPLE); //RITA ÖVER COLORED ORDET I FÖRSTA TILL FÄRG
                    Raylib.DrawText("(PRESS TAB WHILE IN GAME TO PAUSE)", 200, 250, 16, Color.BLACK);
                    Raylib.DrawText("NEW GAME", 170, 300, 24, menuResumeColor);
                    Raylib.DrawText("OPTIONS", 170, 350, 24, menuOptionsColor);
                    Raylib.DrawText("EXIT", 170, 400, 24, menuExitColor);


                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_S)) //KOLLA VAR DEN SKA FLYTTA MARKERINGEN I MENYN
                    {
                        if (menuTarget == 3)
                        {
                            menuTarget = 1;
                        }
                        else
                        {
                            menuTarget++;
                        }
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                    {
                        if (menuTarget == 1)
                        {
                            menuTarget = 3;
                        }
                        else
                        {
                            menuTarget--;
                        }
                    }


                    if (menuTarget == 1) //KOLLA VILKEN AV MENY ALTERNATIVEN SOM ÄR MARKERADE
                    {
                        menuResumeColor = Color.BLACK;
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            gameState = "level_one";
                            level = "one";
                            playerX = 225;
                            playerY = 225; //RESET SPELARENS POSITION IFALL DEN HAR GÅTT TILLBAKA TILL HUVUD MENYN
                            enemyY = 225; //RESET ENEMY POSITION
                            deaths = 0; //RESET ANTAL DEATHS
                        }
                    }
                    else
                    {
                        menuResumeColor = Color.GRAY;
                    }
                    if (menuTarget == 2)
                    {
                        menuOptionsColor = Color.BLACK;
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            gameState = "pause";
                        }
                    }
                    else
                    {
                        menuOptionsColor = Color.GRAY;
                    }
                    if (menuTarget == 3)
                    {
                        menuExitColor = Color.BLACK;
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            Raylib.CloseWindow();
                        }
                    }
                    else
                    {
                        menuExitColor = Color.GRAY;
                    }

                    Raylib.EndDrawing();
                }

                //SPEL LOOP
                if (gameState == "level_one")
                {
                    //SPELAR RÖRELSE
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                    {
                        playerY -= 0.6f;
                        direction = "w";
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                    {
                        playerY += 0.6f;
                        direction = "s";
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    {
                        playerX -= 0.6f;
                        direction = "a";
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    {
                        playerX += 0.6f;
                        direction = "d";
                    }
                    //DIAGONELL HASTIGHET KONTROLL
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    {
                        playerY += 0.2f;
                        playerX -= 0.2f;
                        direction = "dw";
                    }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    {
                        playerY += 0.2f;
                        playerX += 0.2f;
                        direction = "aw";
                    }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    {
                        playerY -= 0.2f;
                        playerX -= 0.2f;
                        direction = "ds";
                    }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    {
                        playerY -= 0.2f;
                        playerX += 0.2f;
                        direction = "as";
                    }
                    //FIENDE HASTIGHET
                    if (enemyY <= 225)
                    {
                        enemySpeed = 1.2f;
                    }
                    else if (enemyY >= 775)
                    {
                        enemySpeed = -1.2f;
                    }

                    enemyY += enemySpeed;

                    //ENEMY COLLISION
                    if (enemyX - playerX <= 75 && enemyX - playerX >= -25 && enemyY - playerY <= 75 && enemyY - playerY >= -25)
                    {
                        playerX = 225;
                        playerY = 225;
                        deaths++;
                        keys = 0;
                    }
                    else if (enemyX + 200 - playerX <= 75 && enemyX + 200 - playerX >= -25 && enemyY - playerY <= 75 && enemyY - playerY >= -25)
                    {
                        playerX = 225;
                        playerY = 225;
                        deaths++;
                        keys = 0;
                    }
                    else if (enemyX + 400 - playerX <= 75 && enemyX + 400 - playerX >= -25 && enemyY - playerY <= 75 && enemyY - playerY >= -25)
                    {
                        playerX = 225;
                        playerY = 225;
                        deaths++;
                        keys = 0;
                    }
                    else if (enemyX + 600 - playerX <= 75 && enemyX + 600 - playerX >= -25 && enemyY - playerY <= 75 && enemyY - playerY >= -25)
                    {
                        playerX = 225;
                        playerY = 225;
                        deaths++;
                        keys = 0;
                    }

                    //VÄGG COLLISION
                    if (playerX > 99 && playerX < 351 && playerY > 99 && playerY < 800) //OM DEN ÄR INOM SPAWN
                    {
                        if (playerX <= 100) //OM DEN ÄR VÄNSTER HÅLL KVAR DEN DÄR
                        {
                            playerX = 100;
                        }
                        else if (playerX >= 350) //OM DEN ÄR HÖGER HÅLL KVAR DEN DÄR
                        {
                            playerX = 350;
                        }
                        if (playerY <= 100) //ÅKER DEN UPP MOT TAKET HÅLL DEN DÄR
                        {
                            playerY = 100;
                        }
                    }
                    else if (playerY > 800 && playerY < 901) //OM DEN ÄR VID UTGÅNGEN
                    {
                        if (playerX <= 100) //HÅLL KVAR VÄNSTER
                        {
                            playerX = 100;
                        }
                        if (playerY >= 850) //HÅLL KVAR BOTTEN
                        {
                            playerY = 850;
                        }
                    }

                    if (playerX > 399 && playerX < 601 && playerY > 799 && playerY < 901) //COLLISION FÖR UTGÅNGEN
                    {
                        if (playerX >= 550)
                        {
                            playerX = 550;
                        }
                        if (playerY >= 850)
                        {
                            playerY = 850;
                        }
                        else if (playerX < 500 && playerY <= 800)
                        {
                            playerY = 800;
                        }
                    }
                    //EXTRA COLLISION BEROENDE PÅ DIREKTION
                    //LEFT CORNER
                    if (direction == "aw" && playerX >= 350 && playerX < 400 && playerY <= 800 && playerY >= 750)
                    {
                        playerY = 800;
                    }
                    else if (direction == "w" && playerX > 350 && playerX < 400 && playerY < 801 && playerY > 799)
                    {
                        playerY = 800;
                    }

                    else if (direction == "dw" && playerX > 350 && playerX <= 400 && playerY < 801 && playerY > 799)
                    {
                        playerY = 800;
                    }

                    //RIGHT CORNER
                    if (direction == "dw" && playerX > 549 && playerX < 600 && playerY >= 750)
                    {
                        playerX = 550;
                    }
                    else if (direction == "d" && playerX > 550 && playerX <= 600 && playerY > 750)
                    {
                        playerX = 550;
                    }

                    if (direction == "ds" && playerX > 549 && playerX < 551 && playerY > 750)
                    {
                        playerX = 550;
                    }

                    else if (direction == "ds" && playerX > 550 && playerX <= 600 && playerY >= 750)
                    {
                        playerY = 750;
                    }

                    if (direction == "as" && playerX > 550 && playerX <= 600 && playerY >= 750)
                    {
                        playerY = 750;
                    }


                    else if (direction == "s" && playerX > 550 && playerX <= 600 && playerY >= 750)
                    {
                        playerY = 750;

                    }


                    //MÅL HÖRN VÄNSTER

                    //MÅL HÖRN HÖGER

                    //RUTNÄTET COLLISION
                    if (playerX > 499 && playerX < 1401 && playerY > 199 && playerY < 800)
                    {
                        if (playerX < 500)
                        {
                            playerX = 500;
                        }
                        else if (playerX >= 1350)
                        {
                            playerX = 1350;
                        }

                        if (playerY <= 200 && playerX < 1300)
                        {
                            playerY = 200;
                        }
                        //KOLLA KOLLISION VID UTGÅNGEN IFALL MAN INTE HAR 2 NYCKLAR
                        if (keys != 2 && playerY <= 200 && playerX <= 1400)
                        {
                            playerY = 200;
                        }

                        else if (playerY >= 750 && playerX >= 600) //KOLLAR ENDAST KOLLISION DÄR VÄGGEN ÄR
                        {
                            playerY = 750;
                        }


                    }
                    //UTGÅNG 2 
                    if (playerX > 1299 && playerX < 1501 && playerY > 99 && playerY < 201) //NÄR SPELAREN ÄR I DE TVÅ UTGÅNGSRUTORNA
                    {
                        if (playerX <= 1300)
                        {
                            playerX = 1300;
                        }
                        if (playerY <= 100)
                        {
                            playerY = 100;
                        }
                        else if (playerY >= 150 && playerX >= 1400) //KOLLAR ENDAST KOLLISION DÄR VÄGGEN ÄR
                        {
                            playerY = 150;
                        }
                    }

                    //KOLLA MÅL COLLISION
                    if (playerX > 1499 && playerX < 1801 && playerY > 99 && playerY < 901)
                    {
                        if (playerX <= 1500 && playerY >= 200)
                        {
                            playerX = 1500;
                        }
                        else if (playerX >= 1750)
                        {
                            playerX = 1750;
                        }

                        if (playerY <= 100)
                        {
                            playerY = 100;
                        }
                        else if (playerY >= 900)
                        {
                            playerY = 900;
                        }
                    }

                    // -------------------------------
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_K))
                    {
                        keys = 2;
                    }
                    //------------------------------------

                    //SE OM SPELAREN KOM I MÅL
                    if (playerX >= 1500 && playerX <= 1800 && playerY >= 700 && playerY <= 900)
                    {
                        completed = true;
                    }

                    if (completed == true)
                    {
                        keys = 0;
                        playerX = 225;
                        playerY = 225;
                        keyOneX = 250;
                        keyOneY = 250;
                        keyTwoX = 250;
                        keyTwoY = 250;
                        gameState = "level_two";
                        level = "two";
                        completed = false;

                    }

                    //GRAFIKER (1 ruta 100px)
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);
                    bool count = true;

                    //DEATH COUNTER
                    Raylib.DrawText("DEATHS: " + deaths, 100, 40, 32, Color.BLACK);
                    Raylib.DrawText("POSITION X: " + playerX + " Y: " + playerY, 300, 40, 32, Color.BLACK);
                    //RUTNÄT
                    //LOOPA MEDANS Y ÄR MELLAN 200 OCH 800 MED 100 ADDITION PER LOOP.
                    for (int y = 200; y < 800; y += 100)
                    {
                        //VARJE Y LOOPA X MELLAN 500 OCH 1400 MED 100 ADDITION PER LOOP
                        for (int x = 500; x < 1400; x += 100)
                        {
                            //RITA EN GUL REKTANGEL OCH SÄTT COUNT TILL FALSE
                            if (count == true)
                            {
                                count = false;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.YELLOW);
                            }
                            //NÄR COUNT ÄR FALSE RITA EN ORANGE KVADRAT OCH SÄTT COUNT TILL TRUE
                            else if (count == false)
                            {
                                count = true;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.ORANGE);
                            }
                        }
                    }
                    //LÄGG TILL DE SISTA KVADRATERNA TILL RUTNÄTET
                    Raylib.DrawRectangle(500, 800, 100, 100, Color.YELLOW);
                    Raylib.DrawRectangle(400, 800, 100, 100, Color.ORANGE);
                    if (keys == 2)
                    {
                        Raylib.DrawRectangle(1300, 100, 100, 100, Color.ORANGE);
                        Raylib.DrawRectangle(1400, 100, 100, 100, Color.YELLOW);
                    }
                    else
                    {
                        Raylib.DrawRectangle(1300, 100, 100, 100, Color.BLACK);
                        Raylib.DrawRectangle(1400, 100, 100, 100, Color.BLACK);
                    }


                    //LÄGG TILL SPAWN OCH SLUT yes.
                    Raylib.DrawRectangle(100, 100, 300, 800, Color.YELLOW);
                    Raylib.DrawRectangle(1500, 100, 300, 800, Color.ORANGE);

                    //LÄGG TILL MÅL RUTA
                    for (int y = 700; y < 900; y += 100)
                    {
                        for (int x = 1500; x < 1800; x += 100)
                        {
                            if (count == true)
                            {
                                count = false;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.WHITE);
                            }
                            else if (count == false)
                            {
                                count = true;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.BLACK);
                            }
                        }
                    }

                    //BORDERS
                    //VÄNSTER SPAWN BORDER
                    Raylib.DrawRectangle(80, 80, 20, 820, Color.BLACK);
                    Raylib.DrawRectangle(80, 900, 520, 20, Color.BLACK);
                    Raylib.DrawRectangle(100, 80, 320, 20, Color.BLACK);
                    Raylib.DrawRectangle(400, 100, 20, 680, Color.BLACK);
                    Raylib.DrawRectangle(400, 780, 100, 20, Color.BLACK);
                    Raylib.DrawRectangle(600, 800, 20, 120, Color.BLACK);

                    //HÖGER BORDER
                    Raylib.DrawRectangle(1800, 80, 20, 820, Color.BLACK);
                    Raylib.DrawRectangle(1300, 80, 500, 20, Color.BLACK);
                    Raylib.DrawRectangle(1500, 900, 320, 20, Color.BLACK);
                    Raylib.DrawRectangle(1480, 200, 20, 720, Color.BLACK);
                    Raylib.DrawRectangle(1400, 200, 100, 20, Color.BLACK);
                    Raylib.DrawRectangle(1280, 80, 20, 120, Color.BLACK);


                    //RUTNÄT BORDER
                    Raylib.DrawRectangle(480, 180, 800, 20, Color.BLACK);
                    Raylib.DrawRectangle(480, 200, 20, 580, Color.BLACK);
                    Raylib.DrawRectangle(620, 800, 780, 20, Color.BLACK);
                    Raylib.DrawRectangle(1400, 220, 20, 600, Color.BLACK);
                    //RITA NYCKEL
                    Raylib.DrawRectangle(keyOneX, keyOneY, 50, 50, Color.GOLD);
                    Raylib.DrawRectangle(keyTwoX, keyTwoY, 50, 50, Color.GOLD);

                    //RITA SPELARE
                    Raylib.DrawRectangle((int)playerX, (int)playerY, 50, 50, Color.BLACK); //OUTLINE
                    Raylib.DrawRectangle((int)playerX + 5, (int)playerY + 5, 40, 40, playerColor);

                    //RITA FIENDER
                    Raylib.DrawCircle((int)enemyX, (int)enemyY, 25, Color.BLACK); //OUTLINE
                    Raylib.DrawCircle((int)enemyX, (int)enemyY, 20, Color.RED);

                    Raylib.DrawCircle((int)enemyX + 200, (int)enemyY, 25, Color.BLACK); //OUTLINE
                    Raylib.DrawCircle((int)enemyX + 200, (int)enemyY, 20, Color.RED);

                    Raylib.DrawCircle((int)enemyX + 400, (int)enemyY, 25, Color.BLACK); //OUTLINE
                    Raylib.DrawCircle((int)enemyX + 400, (int)enemyY, 20, Color.RED);

                    Raylib.DrawCircle((int)enemyX + 600, (int)enemyY, 25, Color.BLACK); //OUTLINE
                    Raylib.DrawCircle((int)enemyX + 600, (int)enemyY, 20, Color.RED);




                    Raylib.EndDrawing();

                    //TESTA OM SPELAREN VILL PAUSA
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "pause";
                    }

                }

                if (gameState == "level_two")
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PINK);
                    Raylib.DrawText("DEATHS: " + deaths, 100, 40, 32, Color.BLACK);
                    Raylib.EndDrawing();

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "pause";
                    }

                }

                if (gameState == "pause")
                {
                    if (gameState == "pause")
                    {
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                        {
                            if (menuTarget == 3)
                            {
                                menuTarget = 1;
                            }
                            else
                            {
                                menuTarget++;
                            }
                        }
                        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                        {
                            if (menuTarget == 1)
                            {
                                menuTarget = 3;
                            }
                            else
                            {
                                menuTarget--;
                            }
                        }

                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.PURPLE);
                        Raylib.DrawText("PAUSED", 900, 200, 32, Color.BLACK);
                        Raylib.DrawText("RESUME", 900, 300, 24, menuResumeColor);
                        Raylib.DrawText("PLAYER COLOR", 900, 350, 24, menuOptionsColor);
                        Raylib.DrawText("MAIN MENU", 900, 400, 24, menuExitColor);

                        if (menuTarget == 1)
                        {
                            menuResumeColor = Color.WHITE;
                            Raylib.DrawRectangle(850, 300, 25, 25, Color.BLACK);
                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                            {
                                gameState = "level_" + level;
                            }

                        }
                        else
                        {
                            menuResumeColor = Color.BLACK;
                        }
                        if (menuTarget == 2)
                        {
                            menuOptionsColor = Color.WHITE;
                            Raylib.DrawRectangle(845, 345, 35, 35, Color.BLACK);
                            Raylib.DrawRectangle(850, 350, 25, 25, playerColor);

                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                            {
                                if (playerArrayIndex == 7)
                                {
                                    playerArrayIndex = 0;
                                }
                                else
                                {
                                    playerArrayIndex++;
                                }
                            }
                        }
                        else
                        {
                            menuOptionsColor = Color.BLACK;
                        }
                        if (menuTarget == 3)
                        {
                            menuExitColor = Color.WHITE;
                            Raylib.DrawRectangle(850, 400, 25, 25, Color.BLACK);
                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                            {
                                gameState = "intro";
                                menuTarget = 1; //SÄTT MARKERING HÖGST UPP I NÄSTA MENY
                            }
                        }
                        else
                        {
                            menuExitColor = Color.BLACK;
                        }


                        Raylib.DrawText("CONTROLS", 200, 200, 32, Color.BLACK);
                        Raylib.DrawText("WASD - MOVEMENT", 170, 300, 24, Color.BLACK);
                        Raylib.DrawText("TAB - PAUSE GAME", 170, 400, 24, Color.BLACK);
                        Raylib.DrawText("ESC - QUIT GAME", 170, 500, 24, Color.BLACK);


                        Raylib.DrawText("OBJECTIVE", 1520, 200, 32, Color.BLACK);
                        Raylib.DrawText("THE OBJECTIVE OF THE GAME ", 1490, 300, 24, Color.BLACK);
                        Raylib.DrawText("IS TO AVOID ENEMIS AND REACH  ", 1490, 350, 24, Color.BLACK);
                        Raylib.DrawText("THE BLACK AND WHITE FINISH", 1490, 400, 24, Color.BLACK);

                        Raylib.DrawText("LINE TO ADVANCE FURTHER.", 1490, 450, 24, Color.BLACK);


                        Raylib.EndDrawing();
                    }

                }
            }

        }
    }
}

