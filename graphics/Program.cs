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

            //BACKGROUND IMAGES
            Texture2D thirdBackground = Raylib.LoadTexture("thirdbackground.png");
            Texture2D secondBackground = Raylib.LoadTexture("secondbackground.png");
            Texture2D firstBackground = Raylib.LoadTexture("firstbackground.png");

            //BARRIER IMAGES
            Texture2D barrier = Raylib.LoadTexture("barrier.png");
            Texture2D barrierTwo = Raylib.LoadTexture("barrier_level2.png");

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
            int keyThreeX = 1425;
            int keyThreeY = 775;

            //ENEMY VALUES
            float enemyX = 650;
            float enemyY = 225;
            float enemySpeed = 1.2f;
            bool playerDead = false;

            //LEVEL, STATE AND GOAL VALUES
            string level = "one";
            string gameState = "intro";
            bool completed = false;

            //MENU COLOR VALUES
            int menuTarget = 1;
            Color menuResumeColor = Color.GRAY;
            Color menuOptionsColor = Color.GRAY;
            Color menuExitColor = Color.GRAY;

            //OPTIONS COLOR VALUES
            bool menuOptionsState = false;
            int menuOptionsTarget = 1;
            Color menuOptionsOne = Color.WHITE;
            Color menuOptionsTwo = Color.WHITE;
            Color menuOptionsThree = Color.WHITE;
            Color menuOptionBoxColor = new Color(68, 68, 68, 175);

            //KEY VALUES
            Color keyOneColor = Color.GOLD;
            Color keyTwoColor = Color.GOLD;
            Color keyThreeColor = Color.GOLD;
            bool keyOneReady = true;
            bool keyTwoReady = true;
            bool keyThreeReady = true;

            //TIMER VALUES
            float timerOne = 0;
            float timerTwo = 0;
            float timerThree = 0;

            //WALL VALUES
            Color wallColor = new Color(176, 32, 32, 255);
            //PLAYER COLOR VALUES
            Color playerColor = Color.PURPLE;
            Color[] playerColors = { Color.RED, Color.BEIGE, Color.BLACK, Color.SKYBLUE, Color.GRAY, Color.GREEN, Color.LIME, Color.MAGENTA, Color.ORANGE, Color.ORANGE, Color.PINK, Color.PURPLE, Color.YELLOW, Color.WHITE };
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

                    if (menuOptionsState == true)
                    {
                        Raylib.DrawRectangle(350, 300, 300, 400, menuOptionBoxColor);
                        Raylib.DrawText("PLAYER COLOR", 370, 320, 24, menuOptionsOne);
                        Raylib.DrawText("MUSIC TRACKS", 370, 370, 24, menuOptionsTwo);
                        Raylib.DrawText("BACK", 370, 420, 24, menuOptionsThree);
                    }

                    if (menuOptionsState == false)
                    {
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
                    }


                    //KOLLA OPTIONS MENY IFALL OPTIONS STATE ÄR SANN
                    else if (menuOptionsState == true)
                    {
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S)) //KOLLA VAR DEN SKA FLYTTA MARKERINGEN I MENYN
                        {
                            if (menuOptionsTarget == 3)
                            {
                                menuOptionsTarget = 1;
                            }
                            else
                            {
                                menuOptionsTarget++;
                            }
                        }
                        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                        {
                            if (menuOptionsTarget == 1)
                            {
                                menuOptionsTarget = 3;
                            }
                            else
                            {
                                menuOptionsTarget--;
                            }
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
                            enemyX = 650; //RESET ENEMY POSITION
                            enemyY = 225;
                            deaths = 0; //RESET ANTAL DEATHS
                            keyOneReady = true;
                            keyTwoReady = true;
                            keyOneColor = Color.GOLD;
                            keyTwoColor = Color.GOLD;
                            keys = 0;
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
                            menuOptionsState = true;

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
                    //MAIN MENU OPTIONS TARGET CHECK
                    if (menuOptionsTarget == 1 && menuOptionsState == true)
                    {
                        menuOptionsOne = Color.BLACK;
                        Raylib.DrawRectangle(590, 315, 35, 35, Color.BLACK);
                        Raylib.DrawRectangle(595, 320, 25, 25, playerColor);

                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            if (playerArrayIndex == 13)
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
                        menuOptionsOne = Color.WHITE;
                    }
                    if (menuOptionsTarget == 2)
                    {
                        menuOptionsTwo = Color.BLACK;
                    }
                    else
                    {
                        menuOptionsTwo = Color.WHITE;
                    }
                    if (menuOptionsTarget == 3)
                    {
                        menuOptionsThree = Color.BLACK;
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            menuOptionsState = false;
                            menuOptionsTarget = 1;
                        }
                    }
                    else
                    {
                        menuOptionsThree = Color.WHITE;
                    }

                    Raylib.EndDrawing();
                }

                //SPEL LOOP
                if (gameState == "level_one")
                {
                    (float pX, float pY, string dir) resultPlayer = PlayerMovement(playerX, playerY, direction);

                    playerX = resultPlayer.pX;
                    playerY = resultPlayer.pY;
                    direction = resultPlayer.dir;

                    //FIENDE HASTIGHET
                    (float eY, float eS, string gState) resultEnemy = EnemyMovement(enemyY, enemySpeed, gameState);

                    enemyY = resultEnemy.eY;
                    enemySpeed = resultEnemy.eS;
                    gameState = resultEnemy.gState;

                    //ENEMY COLLISION
                    (float playX, float playY, float enemX, float enemY, bool pDead, string gState) resultCollision = EnemyCollision(playerX, playerY, enemyX, enemyY, playerDead, gameState);

                    playerX = resultCollision.playX;
                    playerY = resultCollision.playY;
                    enemyX = resultCollision.enemX;
                    enemyY = resultCollision.enemY;
                    playerDead = resultCollision.pDead;
                    gameState = resultCollision.gState;

                    if (playerDead == true)
                    {
                        playerX = 225;
                        playerY = 225;
                        deaths++;
                        keys = 0;
                        keyOneReady = true;
                        keyOneColor = Color.GOLD;
                        keyTwoReady = true;
                        keyTwoColor = Color.GOLD;
                        playerDead = false;
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
                    if (direction == "dw" && playerX >= 1350 && playerX < 1352 && playerY < 200 && playerY > 150)
                    {
                        playerX = 1350;
                    }
                    else if (direction == "d" && playerX >= 1350 && playerX < 1352 && playerY < 200 && playerY > 150)
                    {
                        playerX = 1350;
                    }
                    else if (direction == "s" && playerX > 1350 && playerX < 1400 && playerY < 200 && playerY > 150)
                    {
                        playerY = 150;
                    }
                    else if (direction == "ds" && playerX > 1350 && playerX < 1400 && playerY < 151 && playerY > 149)
                    {
                        playerY = 150;
                    }
                    else if (direction == "ds" && playerX > 1349 && playerX < 1351 && playerY < 200 && playerY > 150)
                    {
                        playerX = 1350;
                    }
                    else if (direction == "as" && playerX > 1350 && playerX < 1400 && playerY < 151 && playerY > 149)
                    {
                        playerY = 150;
                    }

                    //MÅL HÖRN HÖGER
                    if (direction == "aw" && playerX > 1499 && playerX < 1501 && playerY > 149 && playerY < 201)
                    {
                        playerX = 1500;
                    }

                    else if (direction == "as" && playerX > 1499 && playerX < 1501 && playerY > 149 && playerY < 201)
                    {
                        playerX = 1500;
                    }

                    else if (direction == "a" && playerX > 1499 && playerX < 1501 && playerY > 149 && playerY < 201)
                    {
                        playerX = 1500;
                    }

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
                    if (playerX > 1299 && playerX < 1500 && playerY > 99 && playerY < 201) //NÄR SPELAREN ÄR I DE TVÅ UTGÅNGSRUTORNA
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
                        if (playerX <= 1500 && playerY > 200)
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
                    //KOLLA COLLISSION MED NYCKLAR
                    if (keyOneX - playerX > -50 && keyOneX - playerX < 50 && keyOneY - playerY > -50 && keyOneY - playerY < 50 && keyOneReady == true)
                    {
                        keys++;
                        keyOneColor = Color.GREEN;
                        keyOneReady = false;
                    }

                    else if (keyTwoX - playerX > -50 && keyTwoX - playerX < 50 && keyTwoY - playerY > -50 && keyTwoY - playerY < 50 && keyTwoReady == true)
                    {
                        keys++;
                        keyTwoColor = Color.GREEN;
                        keyTwoReady = false;
                    }

                    //SE OM SPELAREN KOM I MÅL
                    if (playerX >= 1500 && playerX <= 1800 && playerY >= 700 && playerY <= 900)
                    {
                        completed = true;
                    }

                    if (completed == true)
                    {
                        playerX = 225;
                        playerY = 475;
                        keyOneReady = true;
                        keyTwoReady = true;
                        keyOneColor = Color.GOLD;
                        keyTwoColor = Color.GOLD;
                        keyOneX = 625;
                        keyOneY = 375;
                        keyTwoX = 1024;
                        keyTwoY = 675;
                        keys = 0;
                        gameState = "level_two";
                        level = "two";
                        completed = false;
                        enemyY = 175;
                        enemyX = 450;


                    }

                    //GRAFIKER (1 ruta 100px)
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);
                    bool count = true;

                    //DEATH COUNTER
                    Raylib.DrawText("DEATHS: " + deaths, 100, 40, 32, Color.BLACK);
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
                        Raylib.DrawRectangle(1300, 100, 100, 100, Color.ORANGE);
                        Raylib.DrawRectangle(1400, 100, 100, 100, Color.YELLOW);
                        Raylib.DrawTextureEx(barrier, new Vector2(1300, 95), 0.0f, 1.0f, Color.WHITE);
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
                                Raylib.DrawRectangle(x, y, 100, 100, Color.RED);
                            }
                            else if (count == false)
                            {
                                count = true;
                                Raylib.DrawRectangle(x, y, 100, 100, wallColor);
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

                    //RITA NYCKLAR
                    Raylib.DrawRectangle(keyOneX, keyOneY, 50, 50, Color.BLACK);
                    Raylib.DrawRectangle(keyTwoX, keyTwoY, 50, 50, Color.BLACK);
                    Raylib.DrawRectangle(keyOneX + 5, keyOneY + 5, 40, 40, keyOneColor);
                    Raylib.DrawRectangle(keyTwoX + 5, keyTwoY + 5, 40, 40, keyTwoColor);

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
                    //PLAYER MOVEMENT METHOD
                    (float pX, float pY, string dir) resultPlayer = PlayerMovement(playerX, playerY, direction);

                    playerX = resultPlayer.pX;
                    playerY = resultPlayer.pY;
                    direction = resultPlayer.dir;
                    //ENEMY MOVEMENT METHOD
                    (float eY, float eS, string gState) resultEnemy = EnemyMovement(enemyY, enemySpeed, gameState);

                    enemyY = resultEnemy.eY;
                    enemySpeed = resultEnemy.eS;
                    gameState = resultEnemy.gState;
                    //ENEMY COLLISION METHOD
                    (float playX, float playY, float enemX, float enemY, bool pDead, string gState) resultCollision = EnemyCollision(playerX, playerY, enemyX, enemyY, playerDead, gameState);

                    playerX = resultCollision.playX;
                    playerY = resultCollision.playY;
                    enemyX = resultCollision.enemX;
                    enemyY = resultCollision.enemY;
                    playerDead = resultCollision.pDead;
                    gameState = resultCollision.gState;
                    //WHAT TO DO WHEN PLAYER DIES
                    if (playerDead == true)
                    {
                        playerX = 225;
                        playerY = 475;
                        deaths++;
                        keys = 0;
                        keyOneReady = true;
                        keyOneColor = Color.GOLD;
                        keyTwoReady = true;
                        keyTwoColor = Color.GOLD;
                        keyThreeReady = true;
                        keyThreeColor = Color.GOLD;
                        playerDead = false;
                        timerOne = 0;
                        timerTwo = 0;
                        timerThree = 0;
                    }

                    //COLLISION VÄGGAR
                    if (playerX > 99 && playerX < 399)
                    {
                        if (playerY <= 350)
                        {
                            playerY = 350;
                        }
                        else if (playerY >= 600)
                        {
                            playerY = 600;
                        }
                        if (playerX <= 100)
                        {
                            playerX = 100;
                        }
                    }
                    //RUTNÄT TOPP COLLISION
                    if (playerX > 399 && playerX < 1451 && playerY < 350 && playerY > 149)
                    {
                        if (playerY <= 150)
                        {
                            playerY = 150;
                        }
                        if (playerX <= 400)
                        {
                            playerX = 400;
                        }
                        else if (playerX >= 1450)
                        {
                            playerX = 1450;
                        }
                    }
                    //RUTNÄT BOT COLLISION
                    if (playerX > 399 && playerX < 1451 && playerY > 600 && playerY < 801)
                    {
                        if (playerX <= 400)
                        {
                            playerX = 400;
                        }
                        else if (playerX >= 1450)
                        {
                            playerX = 1450;
                        }
                        if (playerY >= 800)
                        {
                            playerY = 800;
                        }
                    }
                    //MÅL COLLISION
                    if (playerX > 1500 && playerX < 1751)
                    {
                        if (playerX >= 1750)
                        {
                            playerX = 1750;
                        }
                        if (playerY <= 350)
                        {
                            playerY = 350;
                        }
                        else if (playerY >= 600)
                        {
                            playerY = 600;
                        }
                    }

                    //DIAGONELL COLLISION
                    if (direction == "w" && playerX < 1500 && playerX > 1450 && playerY > 349 && playerY < 351)
                    {
                        playerY = 350;
                    }
                    else if (direction == "aw" && playerX < 1500 && playerX > 1450 && playerY > 349 && playerY < 351)
                    {
                        playerY = 350;
                    }
                    else if (direction == "dw" && playerX < 1500 && playerX > 1450 && playerY > 349 && playerY < 351)
                    {
                        playerY = 350;
                    }

                    if (direction == "s" && playerX < 1500 && playerX > 1450 && playerY > 599 && playerY < 601)
                    {
                        playerY = 600;
                    }
                    else if (direction == "ds" && playerX < 1500 && playerX > 1450 && playerY > 599 && playerY < 601)
                    {
                        playerY = 600;
                    }
                    else if (direction == "as" && playerX < 1500 && playerX > 1450 && playerY > 599 && playerY < 601)
                    {
                        playerY = 600;
                    }
                    //COLLISION MED NYCKLAR
                    if (keyOneX - playerX > -50 && keyOneX - playerX < 50 && keyOneY - playerY > -50 && keyOneY - playerY < 50 && keyOneReady == true)
                    {
                        keys++;
                        keyOneColor = Color.GREEN;
                        keyOneReady = false;
                    }

                    else if (keyTwoX - playerX > -50 && keyTwoX - playerX < 50 && keyTwoY - playerY > -50 && keyTwoY - playerY < 50 && keyTwoReady == true)
                    {
                        keys++;
                        keyTwoColor = Color.GREEN;
                        keyTwoReady = false;
                    }
                    else if (keyThreeX - playerX > -50 && keyThreeX - playerX < 50 && keyThreeY - playerY > -50 && keyThreeY - playerY < 50 && keyThreeReady == true)
                    {
                        keys++;
                        keyThreeColor = Color.GREEN;
                        keyThreeReady = false;
                    }

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.BLACK);
                    bool count = true;

                    //SKAPA SPAWN
                    for (int x = 100; x < 400; x += 100)
                    {
                        for (int y = 350; y < 650; y += 100)
                        {
                            Raylib.DrawRectangle(x, y, 100, 100, Color.RED);
                        }
                    }
                    //SKAPA RUTNÄT
                    for (int x = 400; x < 1500; x += 100)
                    {
                        for (int y = 150; y < 850; y += 100)
                        {
                            if (count == true)
                            {
                                count = false;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.RED);
                            }
                            else if (count == false)
                            {
                                count = true;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.BLACK);
                            }

                        }
                    }
                    //SKAPA MÅL
                    for (int x = 1500; x < 1600; x += 100)
                    {
                        for (int y = 350; y < 650; y += 100)
                        {
                            Raylib.DrawRectangle(x, y, 100, 100, Color.RED);
                        }
                    }

                    //SKAPA MÅL LINJE
                    for (int x = 1600; x < 1800; x += 100)
                    {
                        for (int y = 350; y < 650; y += 100)
                        {
                            if (count == true)
                            {
                                count = false;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.GREEN);
                            }
                            else if (count == false)
                            {
                                count = true;
                                Raylib.DrawRectangle(x, y, 100, 100, Color.DARKGREEN);
                            }
                        }
                    }

                    //KOLLA OM SPELAREN HAR KOMMIT I MÅL
                    if (playerX >= 1600 && playerX < 1800 && playerY > 350 && playerY < 650)
                    {
                        completed = true; //SKAPA GAME STATE 3 MED SCROLLANDE BILDER SOM ILLUSION AV KAMERA
                        //ALLTING I EN VARIABEL OCH NÄR MAN KLICKAR A SÅ SKROLLAS BILDERNA ÅT VÄNSTER.
                    }



                    //SPAWN VÄGGAR
                    Raylib.DrawRectangle(80, 330, 320, 20, wallColor);
                    Raylib.DrawRectangle(80, 350, 20, 300, wallColor);
                    Raylib.DrawRectangle(80, 650, 320, 20, wallColor);
                    //RUTNÄT VÄGGAR
                    Raylib.DrawRectangle(380, 130, 20, 200, wallColor);
                    Raylib.DrawRectangle(380, 670, 20, 200, wallColor);
                    Raylib.DrawRectangle(400, 130, 1100, 20, wallColor);
                    Raylib.DrawRectangle(400, 850, 1100, 20, wallColor);
                    Raylib.DrawRectangle(1500, 130, 20, 220, wallColor);
                    Raylib.DrawRectangle(1500, 650, 20, 220, wallColor);
                    //MÅL VÄGGAR
                    Raylib.DrawRectangle(1520, 330, 300, 20, wallColor);
                    Raylib.DrawRectangle(1520, 650, 300, 20, wallColor);
                    Raylib.DrawRectangle(1800, 350, 20, 300, wallColor);

                    //RITA NYCKLAR
                    Raylib.DrawRectangle(keyOneX, keyOneY, 50, 50, Color.BLACK);
                    Raylib.DrawRectangle(keyTwoX, keyTwoY, 50, 50, Color.BLACK);
                    Raylib.DrawRectangle(keyOneX + 5, keyOneY + 5, 40, 40, keyOneColor);
                    Raylib.DrawRectangle(keyTwoX + 5, keyTwoY + 5, 40, 40, keyTwoColor);
                    Raylib.DrawRectangle(keyThreeX, keyThreeY, 50, 50, Color.BLACK);
                    Raylib.DrawRectangle(keyThreeX + 5, keyThreeY + 5, 40, 40, keyThreeColor);

                    //KEY TIMER
                    if (keyOneReady == false && keys != 3)
                    {
                        timerOne += 1;
                        //RITA TIDEN MAN HAR PÅ SIG KVAR
                        if (timerOne <= 450)
                        {
                            Raylib.DrawText("1", keyOneX + 20, keyOneY + 10, 32, Color.WHITE);
                        }
                        else if (timerOne > 450 && timerOne <= 900)
                        {
                            Raylib.DrawText("2", keyOneX + 20, keyOneY + 10, 32, Color.WHITE);
                        }
                        else if (timerOne > 900 && timerOne <= 1350)
                        {
                            Raylib.DrawText("3", keyOneX + 20, keyOneY + 10, 32, Color.WHITE);
                        }
                        else if (timerOne > 1350 && timerOne <= 1800)
                        {
                            Raylib.DrawText("4", keyOneX + 20, keyOneY + 10, 32, Color.WHITE);
                        }
                        else if (timerOne > 1800 && timerOne <= 2250)
                        {
                            Raylib.DrawText("5", keyOneX + 20, keyOneY + 10, 32, Color.WHITE);
                        }

                        if (timerOne >= 2250)
                        {
                            keys--;
                            keyOneColor = Color.GOLD;
                            keyOneReady = true;
                            timerOne = 0;
                        }
                    }
                    if (keyTwoReady == false && keys != 3)
                    {
                        timerTwo += 1;
                        if (timerTwo <= 450)
                        {
                            Raylib.DrawText("1", keyTwoX + 20, keyTwoY + 10, 32, Color.WHITE);
                        }
                        else if (timerTwo > 450 && timerTwo <= 900)
                        {
                            Raylib.DrawText("2", keyTwoX + 20, keyTwoY + 10, 32, Color.WHITE);
                        }
                        else if (timerTwo > 900 && timerTwo <= 1350)
                        {
                            Raylib.DrawText("3", keyTwoX + 20, keyTwoY + 10, 32, Color.WHITE);
                        }
                        else if (timerTwo > 1350 && timerTwo <= 1800)
                        {
                            Raylib.DrawText("4", keyTwoX + 20, keyTwoY + 10, 32, Color.WHITE);
                        }
                        else if (timerTwo > 1800 && timerTwo <= 2250)
                        {
                            Raylib.DrawText("5", keyTwoX + 20, keyTwoY + 10, 32, Color.WHITE);
                        }
                        if (timerTwo >= 2200)
                        {
                            keys--;
                            keyTwoColor = Color.GOLD;
                            keyTwoReady = true;
                            timerTwo = 0;
                        }
                    }
                    if (keyThreeReady == false && keys != 3)
                    {
                        timerThree += 1;
                        if (timerThree <= 450)
                        {
                            Raylib.DrawText("1", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        else if (timerThree > 450 && timerOne <= 900)
                        {
                            Raylib.DrawText("2", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        else if (timerThree > 900 && timerOne <= 1350)
                        {
                            Raylib.DrawText("3", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        else if (timerThree > 1350 && timerOne <= 1800)
                        {
                            Raylib.DrawText("4", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        else if (timerThree > 1800 && timerOne <= 2250)
                        {
                            Raylib.DrawText("5", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        if (timerThree >= 2200)
                        {
                            keys--;
                            keyThreeColor = Color.GOLD;
                            keyThreeReady = true;
                            timerThree = 0;
                        }
                    }
                    //RITA SPELARE
                    Raylib.DrawRectangle((int)playerX, (int)playerY, 50, 50, Color.BLACK);
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

                    Raylib.DrawCircle((int)enemyX + 800, (int)enemyY, 25, Color.BLACK); //OUTLINE
                    Raylib.DrawCircle((int)enemyX + 800, (int)enemyY, 20, Color.RED);

                    Raylib.DrawCircle((int)enemyX + 1000, (int)enemyY, 25, Color.BLACK); //OUTLINE
                    Raylib.DrawCircle((int)enemyX + 1000, (int)enemyY, 20, Color.RED);

                    //KOLLA OM MAN HAR ALLA NYCKLAR OCH COLLISION
                    if (keys != 3)
                    {
                        Raylib.DrawTextureEx(barrierTwo, new Vector2(1500, 350), 0.0f, 1.0f, Color.WHITE);
                        if (playerX >= 1450)
                        {
                            playerX = 1450;
                        }
                    }

                    Raylib.DrawText("DEATHS: " + deaths, 100, 40, 32, Color.WHITE);
                    Raylib.EndDrawing();

                    if (completed == true)
                    {
                        playerX = 225;
                        playerY = 475;
                        keyOneReady = true;
                        keyTwoReady = true;
                        keyThreeReady = true;
                        keyOneColor = Color.GOLD;
                        keyTwoColor = Color.GOLD;
                        keyThreeColor = Color.GOLD;
                        gameState = "level_three";
                        level = "three";
                        completed = false;
                        enemyY = 175;
                        enemyX = 450;
                    }
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "pause";
                    }
                }

                if (gameState == "level_three")
                {
                    //LOGIK

                    //GRAFIK
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.EndDrawing();
                }
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
                            if (playerArrayIndex == 13)
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
        static (float, float, string) PlayerMovement(float pX, float pY, string dir)
        {
            //SPELAR RÖRELSE (hjälp)
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
            {
                pY -= 0.6f;
                dir = "w";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
            {
                pY += 0.6f;
                dir = "s";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                pX -= 0.6f;
                dir = "a";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                pX += 0.6f;
                dir = "d";
            }
            //DIAGONELL HASTIGHET KONTROLL
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                pY += 0.2f;
                pX -= 0.2f;
                dir = "dw";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                pY += 0.2f;
                pX += 0.2f;
                dir = "aw";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                pY -= 0.2f;
                pX -= 0.2f;
                dir = "ds";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                pY -= 0.2f;
                pX += 0.2f;
                dir = "as";
            }

            return (pX, pY, dir);

        }
        static (float, float, string) EnemyMovement(float eY, float eS, string gState)
        {
            if (gState == "level_one")
            {
                if (eY <= 225)
                {
                    eS = 1.2f;
                }
                else if (eY >= 775)
                {
                    eS = -1.2f;
                }
                eY += eS;
            }
            else if (gState == "level_two")
            {
                if (eY <= 175)
                {
                    eS = 1.8f;
                }
                else if (eY >= 825)
                {
                    eS = -1.8f;
                }
                eY += eS;
            }
            return (eY, eS, gState);
        }
        static (float, float, float, float, bool, string) EnemyCollision(float playX, float playY, float enemX, float enemY, bool pDead, string gState)
        {
            if (gState == "level_one")
            {
                if (enemX - playX <= 75 && enemX - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }

                else if (enemX + 200 - playX <= 75 && enemX + 200 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }
                else if (enemX + 400 - playX <= 75 && enemX + 400 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }
                else if (enemX + 600 - playX <= 75 && enemX + 600 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }
            }

            else if (gState == "level_two")
            {
                if (enemX - playX <= 75 && enemX - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }

                else if (enemX + 200 - playX <= 75 && enemX + 200 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }
                else if (enemX + 400 - playX <= 75 && enemX + 400 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }

                else if (enemX + 600 - playX <= 75 && enemX + 600 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }
                else if (enemX + 800 - playX <= 75 && enemX + 800 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }
                else if (enemX + 1000 - playX <= 75 && enemX + 1000 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                {
                    pDead = true;
                }
            }
            return (playX, playY, enemX, enemY, pDead, gState);
        }


    }
}




