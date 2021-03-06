﻿//ATT GÖRA
//TA BORT MUSIC TRACKS FRÅN INTRO MENYN
//GÖR MUSIK FÖR PAUS OCH SHOP MENYN
//ÅTERSTÄLL MUSIKEN VARJE G^ÅNG MAN BYTER STAGE

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
            //LJUD
            Raylib.InitAudioDevice();
            Raylib.SetMasterVolume(0.3f);
            Music introMusic = Raylib.LoadMusicStream("intro.mp3");
            Music levelOneMusic = Raylib.LoadMusicStream("game2.mp3");
            Music levelTwoMusic = Raylib.LoadMusicStream("game.mp3");
            Music levelThreeIntro = Raylib.LoadMusicStream("game3.mp3");
            Music levelThreeMusic = Raylib.LoadMusicStream("game3new.mp3");
            Music endingMusic = Raylib.LoadMusicStream("ending.mp3");
            Sound deathSound = Raylib.LoadSound("death.mp3");

            //BACKGROUND IMAGES
            Texture2D thirdBackground = Raylib.LoadTexture("thirdbackground.png");
            Texture2D secondBackground = Raylib.LoadTexture("secondbackground.png");
            Texture2D firstBackground = Raylib.LoadTexture("firstbackground.png");

            //BARRIER IMAGES
            Texture2D barrier = Raylib.LoadTexture("barrier.png");
            Texture2D barrierTwo = Raylib.LoadTexture("barrier_level2.png");
            Texture2D barrierThree = Raylib.LoadTexture("barrier_level_three.png");

            //ENDING VALUES
            float endingTextPosition = 1000;
            Color skipColor = Color.BLACK;

            //SHOP VALUES AND IMAGES
            Texture2D speedImage = Raylib.LoadTexture("speedimage.png");
            float speedImageSize = 0.5f;

            //BACKGROUND SCROLL VALUES
            float scrollThird = 0.0f;
            float scrollSecond = 0.0f;
            float scrollFirst = 0.0f;

            //PLAYER VALUES
            float playerX = 225;
            float playerY = 225;
            string direction = "";
            int playerCoins = 0;
            float playerSpeed = 0.6f;

            //GAME VALUES
            int deaths = 0;
            int keys = 0;

            int keyOneX = 825;
            int keyOneY = 425;
            int keyTwoX = 1225;
            int keyTwoY = 625;
            int keyThreeX = 1425;
            int keyThreeY = 775;

            //STAGE 3 VALUES
            int stage = 0;

            //ENEMY VALUES
            float enemyX = 650;
            float enemyY = 225;
            float enemySpeed = 1.2f;
            bool playerDead = false;
            float bossX = 375;
            float bossY = 50;
            float bossSpeedX = 2.6f;
            float bossSpeedY = 2.2f;

            //LEVEL, STATE AND GOAL VALUES
            string level = "one";
            string gameState = "intro";
            bool completed = false;

            //MENU COLOR VALUES
            int menuTarget = 1;
            Color menuResumeColor = Color.GRAY;
            Color menuOptionsColor = Color.GRAY;
            Color menuShopColor = Color.GRAY;
            Color menuExitColor = Color.GRAY;

            //OPTIONS COLOR VALUES
            bool menuOptionsState = false;
            int menuOptionsTarget = 1;
            Color menuOptionsOne = Color.WHITE;
            Color menuOptionsTwo = Color.WHITE;
            Color menuOptionsThree = Color.WHITE;
            Color menuOptionBoxColor = new Color(68, 68, 68, 175);

            //SHOP MENU VALUES
            Color menuShopSpeed = Color.WHITE;
            Color menuShopSkin = Color.WHITE;
            bool speedBought = false;
            bool skinBought = false;

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

            //MUSIC VALUES


            Raylib.SetTargetFPS(450); //KONTROLLERA FPS FÖR ATT SPELARENS HASTIGHET ÄR BEROENDE AV DEN

            while (!Raylib.WindowShouldClose())
            {
                playerColor = playerColors[playerArrayIndex];
                //INTRO LOOP
                if (gameState == "intro")
                {
                    // SPELA INTRO MUSIK
                    Raylib.PlayMusicStream(introMusic);
                    Raylib.UpdateMusicStream(introMusic);
                    Raylib.SetMusicVolume(introMusic, 1.0f);

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
                    //(Extremt inspirerad från internet)
                    Raylib.DrawTextureEx(thirdBackground, new Vector2(scrollThird, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(thirdBackground, new Vector2(thirdBackground.width * 2 + scrollThird, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(secondBackground, new Vector2(scrollSecond, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(secondBackground, new Vector2(secondBackground.width * 2 + scrollSecond, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(firstBackground, new Vector2(scrollFirst, 0), 0.0f, 2.0f, Color.WHITE);
                    Raylib.DrawTextureEx(firstBackground, new Vector2(firstBackground.width * 2 + scrollFirst, 0), 0.0f, 2.0f, Color.WHITE);

                    //INTRO MENY
                    Raylib.DrawText("THE COLORED SQUARE GAME", 200, 200, 32, Color.BLACK);
                    Raylib.DrawText("COLORED", 281, 200, 32, Color.PURPLE); //RITA ÖVER COLORED ORDET I FÖRSTA TILL FÄRG
                    Raylib.DrawText("(PRESS TAB WHILE IN GAME TO PAUSE)", 200, 250, 16, Color.BLACK);
                    Raylib.DrawText("NEW GAME", 170, 300, 24, menuResumeColor);
                    Raylib.DrawText("OPTIONS", 170, 350, 24, menuOptionsColor);
                    Raylib.DrawText("CONTROLS", 170, 400, 24, menuShopColor);
                    Raylib.DrawText("EXIT", 170, 450, 24, menuExitColor);

                    if (menuOptionsState == false)
                    {
                        (int mnTarget, string mngState, int mnStage) resultMenu = MenuTarget(menuTarget, gameState, stage);
                        menuTarget = resultMenu.mnTarget;
                        gameState = resultMenu.mngState;
                        stage = resultMenu.mnStage;
                    }

                    //KOLLA OPTIONS MENY IFALL OPTIONS STATE ÄR SANN
                    else if (menuOptionsState == true)
                    {
                        Raylib.DrawRectangle(350, 300, 300, 400, menuOptionBoxColor);
                        Raylib.DrawText("PLAYER COLOR", 370, 320, 24, menuOptionsOne);
                        Raylib.DrawText("MUSIC TRACKS", 370, 370, 24, menuOptionsTwo);
                        Raylib.DrawText("BACK", 370, 420, 24, menuOptionsThree);
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
                            keyTwoReady = true; //GÖR NYCKLARNA REDO.
                            keyOneColor = Color.GOLD;
                            keyTwoColor = Color.GOLD; //ÄNDRA DERAS FÄRG
                            keyOneX = 825;
                            keyOneY = 425; //RESETTA POSITION 
                            keyTwoX = 1225;
                            keyTwoY = 625;
                            keys = 0;
                            timerOne = 0;
                            timerTwo = 0; //RESETTA TIMERS
                            timerThree = 0;
                            playerCoins = 0;
                            speedBought = false; //RESETTA SHOP UPGRADES
                            skinBought = false;
                            playerSpeed = 0.6f; //TA BORT SHOP UPGRADES FRÅN SPELAREN
                            stage = 0;
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
                        //CONTROLS MENU
                        menuShopColor = Color.BLACK;
                        Raylib.DrawRectangle(925, 200, 400, 500, menuOptionBoxColor);
                        Raylib.DrawText("WASD - MOVEMENT", 1000, 250, 24, Color.WHITE);
                        Raylib.DrawText("TAB - PAUSE", 1000, 300, 24, Color.WHITE);
                        Raylib.DrawText("ESC - QUIT", 1000, 350, 24, Color.WHITE);

                    }
                    else
                    {
                        menuShopColor = Color.GRAY;
                    }
                    if (menuTarget == 4)
                    {
                        menuExitColor = Color.BLACK;
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            break;
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
                    if (menuOptionsTarget == 2 && menuOptionsState == true)
                    {
                        menuOptionsTwo = Color.BLACK;
                    }
                    else
                    {
                        menuOptionsTwo = Color.WHITE;
                    }
                    if (menuOptionsTarget == 3 && menuOptionsState == true)
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
                    (float pX, float pY, string dir, float pSpeed) resultPlayer = PlayerMovement(playerX, playerY, direction, playerSpeed);

                    playerX = resultPlayer.pX;
                    playerY = resultPlayer.pY;
                    direction = resultPlayer.dir;
                    playerSpeed = resultPlayer.pSpeed;

                    //FIENDE HASTIGHET
                    (float eY, float eS, string gState) resultEnemy = EnemyMovement(enemyY, enemySpeed, gameState);

                    enemyY = resultEnemy.eY;
                    enemySpeed = resultEnemy.eS;
                    gameState = resultEnemy.gState;

                    //ENEMY COLLISION
                    (float playX, float playY, float enemX, float enemY, bool pDead, string gState, int mStage) resultCollision = EnemyCollision(playerX, playerY, enemyX, enemyY, playerDead, gameState, stage);

                    playerX = resultCollision.playX;
                    playerY = resultCollision.playY;
                    enemyX = resultCollision.enemX;
                    enemyY = resultCollision.enemY;
                    playerDead = resultCollision.pDead;
                    gameState = resultCollision.gState;
                    stage = resultCollision.mStage;
                    //MUSIK LEVEL ONE
                    Raylib.PlayMusicStream(levelOneMusic);
                    Raylib.UpdateMusicStream(levelOneMusic);
                    Raylib.SetMusicVolume(levelOneMusic, 0.3f);
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
                        Raylib.PlaySound(deathSound);
                        Raylib.SetSoundVolume(deathSound, 0.3f);
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
                        playerCoins++;

                    }

                    //GRAFIKER (1 ruta 100px)
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);
                    bool count = true;

                    //GUI LEVEL ONE
                    Raylib.DrawRectangle(0, 0, 1950, 50, Color.BLACK);
                    Raylib.DrawText("KEYS: ", 50, 10, 32, Color.WHITE);
                    Raylib.DrawRectangle(170, 12, 25, 25, keyOneColor);
                    Raylib.DrawRectangle(220, 12, 25, 25, keyTwoColor);
                    Raylib.DrawText("DEATHS: " + deaths, 375, 10, 32, Color.WHITE);
                    Raylib.DrawText("COINS: " + playerCoins, 675, 10, 32, Color.WHITE);
                    Raylib.DrawText("COLOR: ", 975, 10, 32, Color.WHITE);
                    Raylib.DrawRectangle(1125, 12, 25, 25, playerColor);

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
                    (float pX, float pY, string dir, float pSpeed) resultPlayer = PlayerMovement(playerX, playerY, direction, playerSpeed);

                    playerX = resultPlayer.pX;
                    playerY = resultPlayer.pY;
                    direction = resultPlayer.dir;
                    playerSpeed = resultPlayer.pSpeed;
                    //ENEMY MOVEMENT METHOD
                    (float eY, float eS, string gState) resultEnemy = EnemyMovement(enemyY, enemySpeed, gameState);

                    enemyY = resultEnemy.eY;
                    enemySpeed = resultEnemy.eS;
                    gameState = resultEnemy.gState;
                    //ENEMY COLLISION METHOD
                    (float playX, float playY, float enemX, float enemY, bool pDead, string gState, int mStage) resultCollision = EnemyCollision(playerX, playerY, enemyX, enemyY, playerDead, gameState, stage);

                    playerX = resultCollision.playX;
                    playerY = resultCollision.playY;
                    enemyX = resultCollision.enemX;
                    enemyY = resultCollision.enemY;
                    playerDead = resultCollision.pDead;
                    gameState = resultCollision.gState;
                    stage = resultCollision.mStage;
                    //MUSIK LEVEL TWO
                    Raylib.PlayMusicStream(levelTwoMusic);
                    Raylib.UpdateMusicStream(levelTwoMusic);
                    Raylib.SetMusicVolume(levelTwoMusic, 0.9f);

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
                        Raylib.PlaySound(deathSound);
                        Raylib.SetSoundVolume(deathSound, 0.3f);
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
                        completed = true;
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
                    Raylib.DrawRectangle(keyOneX + 5, keyOneY + 5, 40, 40, keyOneColor);
                    Raylib.DrawRectangle(keyTwoX, keyTwoY, 50, 50, Color.BLACK);
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
                        else if (timerThree > 450 && timerThree <= 900)
                        {
                            Raylib.DrawText("2", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        else if (timerThree > 900 && timerThree <= 1350)
                        {
                            Raylib.DrawText("3", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        else if (timerThree > 1350 && timerThree <= 1800)
                        {
                            Raylib.DrawText("4", keyThreeX + 20, keyThreeY + 10, 32, Color.WHITE);
                        }
                        else if (timerThree > 1800 && timerThree <= 2250)
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

                    //GUI LEVEL TWO
                    Raylib.DrawRectangle(0, 0, 1950, 50, Color.RED);
                    Raylib.DrawText("KEYS: ", 50, 10, 32, Color.BLACK);
                    Raylib.DrawRectangle(170, 12, 25, 25, keyOneColor);
                    Raylib.DrawRectangle(220, 12, 25, 25, keyTwoColor);
                    Raylib.DrawRectangle(270, 12, 25, 25, keyThreeColor);
                    Raylib.DrawText("DEATHS: " + deaths, 375, 10, 32, Color.BLACK);
                    Raylib.DrawText("COINS: " + playerCoins, 675, 10, 32, Color.BLACK);
                    Raylib.DrawText("COLOR: ", 975, 10, 32, Color.BLACK);
                    Raylib.DrawRectangle(1125, 12, 25, 25, playerColor);
                    Raylib.EndDrawing();

                    if (completed == true)
                    {
                        keyOneReady = true;
                        keyTwoReady = true;
                        keyThreeReady = true;
                        keyOneColor = Color.GOLD;
                        keyTwoColor = Color.GOLD;
                        keyThreeColor = Color.GOLD;
                        keys = 0;
                        gameState = "level_three";
                        level = "three";
                        completed = false;
                        enemyY = 75;
                        enemyX = 650;
                        playerCoins++;
                    }
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "pause";
                    }
                }

                if (gameState == "level_three")
                {
                    bool count = true;
                    //LOGIK
                    (float pX, float pY, string dir, float pSpeed) resultPlayer = PlayerMovement(playerX, playerY, direction, playerSpeed);

                    playerX = resultPlayer.pX;
                    playerY = resultPlayer.pY;
                    direction = resultPlayer.dir;
                    playerSpeed = resultPlayer.pSpeed;

                    //FIENDE HASTIGHET
                    (float eY, float eS, string gState) resultEnemy = EnemyMovement(enemyY, enemySpeed, gameState);

                    enemyY = resultEnemy.eY;
                    enemySpeed = resultEnemy.eS;
                    gameState = resultEnemy.gState;

                    //ENEMY COLLISION
                    (float playX, float playY, float enemX, float enemY, bool pDead, string gState, int mStage) resultCollision = EnemyCollision(playerX, playerY, enemyX, enemyY, playerDead, gameState, stage);

                    playerX = resultCollision.playX;
                    playerY = resultCollision.playY;
                    enemyX = resultCollision.enemX;
                    enemyY = resultCollision.enemY;
                    playerDead = resultCollision.pDead;
                    gameState = resultCollision.gState;
                    stage = resultCollision.mStage;

                    if (keyOneX - playerX > -50 && keyOneX - playerX < 50 && keyOneY - playerY > -50 && keyOneY - playerY < 50 && keyOneReady == true && stage == 1)
                    {
                        keys++;
                        keyOneColor = Color.GREEN;
                        keyOneReady = false;
                    }

                    else if (keyTwoX - playerX > -50 && keyTwoX - playerX < 50 && keyTwoY - playerY > -50 && keyTwoY - playerY < 50 && keyTwoReady == true && stage == 2)
                    {
                        keys++;
                        keyTwoColor = Color.GREEN;
                        keyTwoReady = false;
                    }
                    else if (keyThreeX - playerX > -50 && keyThreeX - playerX < 50 && keyThreeY - playerY > -50 && keyThreeY - playerY < 50 && keyThreeReady == true && stage == 3)
                    {
                        keys++;
                        keyThreeColor = Color.GREEN;
                        keyThreeReady = false;
                    }

                    //BOSS COLLISION
                    if (stage == 1)
                    {
                        if (bossY <= 75)
                        {
                            bossSpeedY = 1.4f;
                        }
                        else if (bossY >= 925)
                        {
                            bossSpeedY = -1.4f;
                        }
                        if (bossX <= 45)
                        {
                            bossSpeedX = 1.8f;
                        }
                        else if (bossX >= 1895)
                        {
                            bossSpeedX = -1.8f;
                        }
                    }
                    else if (stage == 2)
                    {
                        if (bossY <= 75)
                        {
                            bossSpeedY = 1.4f;
                        }
                        else if (bossY >= 925)
                        {
                            bossSpeedY = -1.4f;
                        }
                        if (bossX <= 25)
                        {
                            bossSpeedX = 1.8f;
                        }
                        else if (bossX >= 1895)
                        {
                            bossSpeedX = -1.8f;
                        }
                        //COLLISION MED SLUT
                        if (bossY >= 350 && bossY <= 650 && bossX >= 740 && bossX <= 745)
                        {
                            bossSpeedX = -1.8f;
                        }
                        else if (bossY >= 350 && bossY <= 650 && bossX <= 990 && bossX >= 985)
                        {
                            bossSpeedX = 1.8f;
                        }
                        if (bossX >= 765 && bossX <= 965 && bossY >= 325 && bossY <= 330)
                        {
                            bossSpeedY = -1.4f;
                        }
                        else if (bossX >= 765 && bossX <= 965 && bossY <= 675 && bossY >= 670)
                        {
                            bossSpeedY = 1.4f;
                        }

                    }
                    else if (stage == 3)
                    {
                        if (bossY <= 75)
                        {
                            bossSpeedY = 1.4f;
                        }
                        else if (bossY >= 925)
                        {
                            bossSpeedY = -1.4f;
                        }
                        if (bossX <= 25)
                        {
                            bossSpeedX = 1.8f;
                        }
                        else if (bossX >= 1875)
                        {
                            bossSpeedX = -1.8f;
                        }
                    }
                    bossX += bossSpeedX;
                    bossY += bossSpeedY;

                    //BOSS OCH SPELARE KOLLISION
                    if (bossX - playerX <= 75 && bossX - playerX >= -25 && bossY - playerY <= 75 && bossY - playerY >= -25)
                    {
                        playerDead = true;
                    }


                    Random generator = new Random();

                    if (stage == 0)
                    {
                        //MUSIK LEVEL THREE
                        Raylib.PlayMusicStream(levelThreeIntro);
                        Raylib.UpdateMusicStream(levelThreeIntro);
                        Raylib.SetMusicVolume(levelThreeIntro, 1.0f);
                        //GRAFIK
                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.RED);
                        Raylib.DrawText("Are you sure you want to continiue?", 600, 200, 32, Color.BLACK);
                        Raylib.DrawText("This last level is kind of difficult.", 600, 250, 32, Color.BLACK);
                        Raylib.DrawText("CONTINIUE", 600, 300, 24, menuResumeColor);
                        Raylib.DrawText("MAIN MENU", 800, 300, 24, menuExitColor);
                        Raylib.DrawText("SHOP", 1000, 300, 24, menuShopColor);

                        (int mnTarget, string mngState, int mnStage) resultMenu = MenuTarget(menuTarget, gameState, stage);
                        menuTarget = resultMenu.mnTarget;
                        gameState = resultMenu.mngState;
                        stage = resultMenu.mnStage;
                        //KOLLA VAR DEN ÄR OCH VAD DEN VILL GÖRA
                        if (menuTarget == 1)
                        {
                            menuResumeColor = Color.BLACK;
                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                            {
                                //SÄTT NYA VÄRDEN PÅ NYCKLAR
                                keyOneX = 1045;
                                keyOneY = 275;
                                keyTwoX = 990;
                                keyTwoY = 475;
                                keyThreeX = 825;
                                //BYT STAGE
                                stage = generator.Next(1, 4);
                                if (stage == 1)
                                {
                                    playerX = 345; //ÄNDRAR SPELARENS STARTPOSITION BEROENDE PÅ VILKEN STAGE SOM SLUMPAS
                                    playerY = 475;
                                }
                                else if (stage == 2)
                                {
                                    playerX = 840;
                                    playerY = 125;
                                }
                                else if (stage == 3)
                                {
                                    playerX = 1525;
                                    playerY = 475;
                                }
                                bossX = 250;
                                bossY = 60;
                            }
                        }
                        else
                        {
                            menuResumeColor = Color.WHITE;
                        }
                        if (menuTarget == 2)
                        {
                            menuExitColor = Color.BLACK;
                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                            {
                                gameState = "intro";
                                level = "one";
                            }
                        }
                        else
                        {
                            menuExitColor = Color.WHITE;
                        }
                        if (menuTarget == 3)
                        {
                            menuShopColor = Color.BLACK;
                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                            {
                                gameState = "shop";
                            }
                        }
                        else
                        {
                            menuShopColor = Color.WHITE;
                        }

                        Raylib.EndDrawing();
                    }
                    if (stage == 1)
                    {
                        //MUSIK LEVEL THREE
                        Raylib.PlayMusicStream(levelThreeMusic);
                        Raylib.UpdateMusicStream(levelThreeMusic);
                        Raylib.SetMusicVolume(levelThreeMusic, 1.0f);
                        //COLLISION STAGE 1
                        if (playerX <= 20)
                        {
                            playerX = 20;
                        }
                        else if (playerX >= 1900)
                        {
                            stage = 2;
                            playerX = 0;
                        }
                        if (playerY <= 50)
                        {
                            playerY = 50;
                        }
                        else if (playerY >= 900)
                        {
                            playerY = 900;
                        }

                        if (playerDead == true)
                        {
                            playerX = 345;
                            playerY = 475;
                            deaths++;
                            keys = 0;
                            keyOneReady = true;
                            keyOneColor = Color.GOLD;
                            keyTwoReady = true;
                            keyTwoColor = Color.GOLD;
                            keyThreeColor = Color.GOLD;
                            keyThreeReady = true;
                            playerDead = false;
                            Raylib.PlaySound(deathSound);
                            Raylib.SetSoundVolume(deathSound, 0.3f);
                        }
                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.DARKPURPLE);

                        //RUTNÄT
                        for (int y = 50; y < 950; y += 100)
                        {
                            for (int x = 20; x < 1920; x += 100)
                            {
                                //KONTROLLERA SÅ DE BLIR VARANNAN RUTA
                                if (count == true)
                                {
                                    count = false;
                                    Raylib.DrawRectangle(x, y, 100, 100, Color.WHITE);
                                }
                                else if (count == false)
                                {
                                    count = true;
                                    Raylib.DrawRectangle(x, y, 100, 100, Color.PURPLE);
                                }
                            }
                        }
                        //SPAWN
                        for (int y = 350; y < 650; y += 100)
                        {
                            for (int x = 220; x < 520; x += 100)
                            {
                                Raylib.DrawRectangle(x, y, 100, 100, Color.GREEN);
                            }
                        }
                        //RITA NYCKEL STAGE 1
                        Raylib.DrawRectangle(keyOneX, keyOneY, 50, 50, Color.BLACK);
                        Raylib.DrawRectangle(keyOneX + 5, keyOneY + 5, 40, 40, keyOneColor);

                        Raylib.DrawRectangle((int)playerX, (int)playerY, 50, 50, Color.BLACK);
                        Raylib.DrawRectangle((int)playerX + 5, (int)playerY + 5, 40, 40, playerColor);

                        //RITA FIENDER STAGE 1
                        Raylib.DrawCircle((int)enemyX + 20, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 20, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 220, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 220, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 420, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 420, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 620, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 620, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 820, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 820, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 1020, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 1020, (int)enemyY, 20, Color.RED);

                        //RITA BOSS 
                        Raylib.DrawCircle((int)bossX, (int)bossY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)bossX, (int)bossY, 20, Color.PURPLE);

                        //GUI LEVEL THREE
                        Raylib.DrawText("KEYS: ", 50, 10, 32, Color.WHITE);
                        Raylib.DrawRectangle(170, 12, 25, 25, keyOneColor);
                        Raylib.DrawRectangle(220, 12, 25, 25, keyTwoColor);
                        Raylib.DrawRectangle(270, 12, 25, 25, keyThreeColor);
                        Raylib.DrawText("DEATHS: " + deaths, 375, 10, 32, Color.WHITE);
                        Raylib.DrawText("COINS: " + playerCoins, 675, 10, 32, Color.WHITE);
                        Raylib.DrawText("COLOR: ", 975, 10, 32, Color.WHITE);
                        Raylib.DrawRectangle(1125, 12, 25, 25, playerColor);
                        Raylib.EndDrawing();
                    }
                    else if (stage == 2)
                    {
                        //MUSIK LEVEL THREE
                        Raylib.PlayMusicStream(levelThreeMusic);
                        Raylib.UpdateMusicStream(levelThreeMusic);
                        Raylib.SetMusicVolume(levelThreeMusic, 1.0f);
                        //COLLISION STAGE 2
                        if (playerY <= 50)
                        {
                            playerY = 50;
                        }
                        else if (playerY >= 900)
                        {
                            playerY = 900;
                        }
                        if (playerX <= -20)
                        {
                            stage = 1;
                            playerX = 1850;
                        }
                        else if (playerX >= 1900)
                        {
                            stage = 3;
                            playerX = 0;
                        }

                        //RESET IF PLAYER DIES
                        if (playerDead == true)
                        {
                            playerX = 840;
                            playerY = 125;
                            deaths++;
                            keys = 0;
                            keyOneReady = true;
                            keyOneColor = Color.GOLD;
                            keyTwoReady = true;
                            keyTwoColor = Color.GOLD;
                            keyThreeColor = Color.GOLD;
                            keyThreeReady = true;
                            playerDead = false;
                            Raylib.PlaySound(deathSound);
                            Raylib.SetSoundVolume(deathSound, 0.3f);
                        }

                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.DARKPURPLE);

                        //GUI LEVEL THREE
                        Raylib.DrawText("KEYS: ", 50, 10, 32, Color.WHITE);
                        Raylib.DrawRectangle(170, 12, 25, 25, keyOneColor);
                        Raylib.DrawRectangle(220, 12, 25, 25, keyTwoColor);
                        Raylib.DrawRectangle(270, 12, 25, 25, keyThreeColor);
                        Raylib.DrawText("DEATHS: " + deaths, 375, 10, 32, Color.WHITE);
                        Raylib.DrawText("COINS: " + playerCoins, 675, 10, 32, Color.WHITE);
                        Raylib.DrawText("COLOR: ", 975, 10, 32, Color.WHITE);
                        Raylib.DrawRectangle(1125, 12, 25, 25, playerColor);

                        for (int y = 50; y < 950; y += 100)
                        {
                            //VARJE Y LOOPA X MELLAN 500 OCH 1400 MED 100 ADDITION PER LOOP
                            for (int x = -35; x < 2065; x += 100)
                            {
                                //RITA EN GUL REKTANGEL OCH SÄTT COUNT TILL FALSE
                                if (count == true)
                                {
                                    count = false;
                                    Raylib.DrawRectangle(x, y, 100, 100, Color.WHITE);
                                }
                                //NÄR COUNT ÄR FALSE RITA EN ORANGE KVADRAT OCH SÄTT COUNT TILL TRUE
                                else if (count == false)
                                {
                                    count = true;
                                    Raylib.DrawRectangle(x, y, 100, 100, Color.PURPLE);
                                }
                            }
                        }
                        //RITA UT MÅL
                        Raylib.DrawRectangle(765, 350, 200, 300, Color.DARKGRAY);
                        Raylib.DrawText("THE END", 790, 470, 32, Color.WHITE);
                        //RITA UT BORDER OCH SKAPA COLLISION OM SPELAREN INTE HAR ALLA NYCKLAR
                        if (keys != 3)
                        {
                            Raylib.DrawTextureEx(barrierThree, new Vector2(765, 350), 0.0f, 1.0f, Color.WHITE);
                            //KOLLA OM SPELAREN ÄR INOM MÅLOMRÅDET OCH VILKET HÅLL DEN KOMMER IFRÅN
                            if (playerX > 715 && playerX < 965 && playerY >= 301 && playerY <= 649)
                            {
                                if (direction == "d" || direction == "dw" || direction == "ds")
                                {
                                    playerX = 715;
                                }
                                else if (direction == "a" || direction == "aw" || direction == "as")
                                {
                                    playerX = 965;
                                }
                            }
                            if (playerX > 715 && playerX < 965 && playerY > 649 && playerY <= 650)
                            {
                                playerY = 650;
                            }
                            else if (playerX > 715 && playerX < 965 && playerY >= 300 && playerY < 302)
                            {
                                playerY = 300;
                            }

                        }
                        //RITA SPAWN
                        Raylib.DrawRectangle(765, 50, 200, 200, Color.GREEN);
                        //RITA NYCKEL STAGE 2
                        Raylib.DrawRectangle(keyTwoX, keyTwoY, 50, 50, Color.BLACK);
                        Raylib.DrawRectangle(keyTwoX + 5, keyTwoY + 5, 40, 40, keyTwoColor);
                        //SPELARE
                        Raylib.DrawRectangle((int)playerX, (int)playerY, 50, 50, Color.BLACK);
                        Raylib.DrawRectangle((int)playerX + 5, (int)playerY + 5, 40, 40, playerColor);
                        //FIENDER STAGE 2
                        Raylib.DrawCircle((int)enemyX - 335, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX - 335, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX - 135, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX - 135, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 65, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 65, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 365, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 365, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 565, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 565, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 765, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 765, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 965, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 965, (int)enemyY, 20, Color.RED);
                        //RITA BOSS STAGE 2
                        Raylib.DrawCircle((int)bossX, (int)bossY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)bossX, (int)bossY, 20, Color.PURPLE);

                        Raylib.EndDrawing();

                        //TITTA OM SPELAREN HAR KLARAT NIVÅN
                        if (keys == 3)
                        {
                            if (playerX > 765 && playerX < 915 && playerY > 350 && playerY < 600)
                            {
                                completed = true;
                            }
                        }

                        if (completed == true)
                        {
                            menuTarget = 1;
                            gameState = "ending";
                            completed = false;
                        }
                    }
                    //STAGE 3
                    else if (stage == 3)
                    {
                        //MUSIK LEVEL THREE
                        Raylib.PlayMusicStream(levelThreeMusic);
                        Raylib.UpdateMusicStream(levelThreeMusic);
                        Raylib.SetMusicVolume(levelThreeMusic, 1.0f);
                        if (playerX <= -20)
                        {
                            stage = 2;
                            playerX = 1850;
                        }
                        if (playerX >= 1850)
                        {
                            playerX = 1850;
                        }
                        if (playerY <= 50)
                        {
                            playerY = 50;
                        }
                        else if (playerY >= 900)
                        {
                            playerY = 900;
                        }
                        //PLAYERDEAD
                        if (playerDead == true)
                        {
                            playerX = 1525;
                            playerY = 475;
                            deaths++;
                            keys = 0;
                            keyOneReady = true;
                            keyOneColor = Color.GOLD;
                            keyTwoReady = true;
                            keyTwoColor = Color.GOLD;
                            keyThreeColor = Color.GOLD;
                            keyThreeReady = true;
                            playerDead = false;
                            Raylib.PlaySound(deathSound);
                            Raylib.SetSoundVolume(deathSound, 0.3f);
                        }
                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.DARKPURPLE);

                        //GUI LEVEL THREE
                        Raylib.DrawText("KEYS: ", 50, 10, 32, Color.WHITE);
                        Raylib.DrawRectangle(170, 12, 25, 25, keyOneColor);
                        Raylib.DrawRectangle(220, 12, 25, 25, keyTwoColor);
                        Raylib.DrawRectangle(270, 12, 25, 25, keyThreeColor);
                        Raylib.DrawText("DEATHS: " + deaths, 375, 10, 32, Color.WHITE);
                        Raylib.DrawText("COINS: " + playerCoins, 675, 10, 32, Color.WHITE);
                        Raylib.DrawText("COLOR: ", 975, 10, 32, Color.WHITE);
                        Raylib.DrawRectangle(1125, 12, 25, 25, playerColor);

                        for (int y = 50; y < 950; y += 100)
                        {
                            for (int x = 0; x < 1900; x += 100)
                            {
                                //KONTROLLERA SÅ DE BLIR VARANNAN RUTA
                                if (count == true)
                                {
                                    count = false;
                                    Raylib.DrawRectangle(x, y, 100, 100, Color.WHITE);
                                }
                                else if (count == false)
                                {
                                    count = true;
                                    Raylib.DrawRectangle(x, y, 100, 100, Color.PURPLE);
                                }
                            }
                        }
                        //SPAWN  2
                        for (int y = 350; y < 650; y += 100)
                        {
                            for (int x = 1400; x < 1700; x += 100)
                            {
                                Raylib.DrawRectangle(x, y, 100, 100, Color.GREEN);
                            }
                        }
                        //RITA NYCKEL STAGE 3
                        Raylib.DrawRectangle(keyThreeX, keyThreeY, 50, 50, Color.BLACK);
                        Raylib.DrawRectangle(keyThreeX + 5, keyThreeY + 5, 40, 40, keyThreeColor);

                        Raylib.DrawRectangle((int)playerX, (int)playerY, 50, 50, Color.BLACK);
                        Raylib.DrawRectangle((int)playerX + 5, (int)playerY + 5, 40, 40, playerColor);

                        Raylib.DrawCircle((int)enemyX - 400, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX - 400, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX - 200, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX - 200, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 200, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 200, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 400, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 400, (int)enemyY, 20, Color.RED);

                        Raylib.DrawCircle((int)enemyX + 600, (int)enemyY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)enemyX + 600, (int)enemyY, 20, Color.RED);
                        //RITA BOSS STAGE 3
                        Raylib.DrawCircle((int)bossX, (int)bossY, 25, Color.BLACK); //OUTLINE
                        Raylib.DrawCircle((int)bossX, (int)bossY, 20, Color.PURPLE);

                        Raylib.EndDrawing();
                    }

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "pause";
                    }
                }
                if (gameState == "ending")
                {
                    //MUSIK ENDING
                    Raylib.PlayMusicStream(endingMusic);
                    Raylib.UpdateMusicStream(endingMusic);
                    Raylib.SetMusicVolume(endingMusic, 1.0f);

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);
                    //SCROLLING END CREDITS
                    Raylib.DrawText("You completed the game!", 350, (int)endingTextPosition, 64, Color.BLACK);
                    Raylib.DrawText("Thank you for playing and I hope you enjoyed it.", 350, (int)endingTextPosition + 200, 48, Color.BLACK);
                    Raylib.DrawText("Graphics created by:", 350, (int)endingTextPosition + 300, 32, Color.BLACK);
                    Raylib.DrawText("noodle3", 350, (int)endingTextPosition + 375, 32, Color.BLACK);
                    Raylib.DrawText("Music created by:", 350, (int)endingTextPosition + 475, 32, Color.BLACK);
                    Raylib.DrawText("noodle3", 350, (int)endingTextPosition + 550, 32, Color.BLACK);
                    Raylib.DrawText("Game content created by:", 350, (int)endingTextPosition + 650, 32, Color.BLACK);
                    Raylib.DrawText("noodle3", 350, (int)endingTextPosition + 725, 32, Color.BLACK);
                    //SKIP 
                    Raylib.DrawText("SKIP [ C ]", 1830, 950, 16, skipColor);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_C) && endingTextPosition > -725)
                    {
                        endingTextPosition = -725;
                        skipColor = Color.WHITE;
                    }
                    if (endingTextPosition < -725)
                    {
                        skipColor = Color.WHITE;
                        //DEATHS
                        Raylib.DrawText("You finished the game with " + deaths + " deaths...", 250, 250, 32, Color.BLACK);
                        Raylib.DrawText("" + deaths, 687, 250, 32, Color.RED);
                        if (deaths >= 0 && deaths <= 20)
                        {
                            Raylib.DrawText("That's kind of good I guess, depending on your time which I'm not aware of.", 250, 300, 32, Color.BLACK);
                        }
                        else if (deaths > 20 && deaths <= 40)
                        {
                            Raylib.DrawText("Congratulations, I guess? You should be able to cut down to at least 15.", 250, 300, 32, Color.BLACK);
                        }
                        else if (deaths > 40 && deaths <= 60)
                        {
                            Raylib.DrawText("That's rough.", 250, 300, 32, Color.BLACK);
                            Raylib.DrawText("If you reset the game and finish it in less deaths nobody would know...", 250, 350, 32, Color.BLACK);
                        }
                        else if (deaths > 60 && deaths <= 70)
                        {
                            Raylib.DrawText("I don't even know what to say...", 250, 300, 32, Color.BLACK);
                            Raylib.DrawText("I already used my best disses at way lower death rates.", 250, 350, 32, Color.BLACK);
                        }
                        else if (deaths > 70 && deaths <= 80)
                        {
                            Raylib.DrawText("Maybe I should've added an easier difficulty for players like you.", 250, 300, 32, Color.BLACK);
                            Raylib.DrawText("That was an oversight from my part, I apologize.", 250, 350, 32, Color.BLACK);
                        }
                        else if (deaths > 80)
                        {
                            Raylib.DrawText("Your death number is way too high", 250, 300, 32, Color.BLACK);
                            Raylib.DrawText("if you want this text to mean anything you really need to die less.", 250, 350, 32, Color.BLACK);
                        }
                        Raylib.DrawText("Play again", 350, 450, 32, menuResumeColor);
                        Raylib.DrawText("Exit", 550, 450, 32, menuExitColor);
                        //MENU MOVEMENT FÖR SLUT MENYN
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                        {
                            if (menuTarget == 1)
                            {
                                menuTarget = 2;
                            }
                            else
                            {
                                menuTarget--;
                            }
                        }
                        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                        {
                            if (menuTarget == 2)
                            {
                                menuTarget = 1;
                            }
                            else
                            {
                                menuTarget++;
                            }
                        }

                        if (menuTarget == 1)
                        {
                            menuResumeColor = Color.BLACK;
                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                            {
                                gameState = "intro";
                                playerCoins = 0;
                                deaths = 0;
                                speedBought = false;
                                skinBought = false;
                            }
                        }
                        else
                        {
                            menuResumeColor = Color.GRAY;
                        }
                        if (menuTarget == 2)
                        {
                            menuExitColor = Color.BLACK;
                            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                            {
                                break;
                            }
                        }
                        else
                        {
                            menuExitColor = Color.GRAY;
                        }
                    }

                    Raylib.EndDrawing();
                    endingTextPosition -= 0.1f;
                }

                if (gameState == "pause")
                {
                    (int mnTarget, string mngState, int mnStage) resultMenu = MenuTarget(menuTarget, gameState, stage);
                    menuTarget = resultMenu.mnTarget;
                    gameState = resultMenu.mngState;
                    stage = resultMenu.mnStage;

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.RED);
                    Raylib.DrawText("PAUSED", 900, 200, 32, Color.BLACK);
                    Raylib.DrawText("RESUME", 900, 300, 24, menuResumeColor);
                    Raylib.DrawText("PLAYER COLOR", 900, 350, 24, menuOptionsColor);
                    Raylib.DrawText("SHOP", 900, 400, 24, menuShopColor);
                    Raylib.DrawText("MAIN MENU", 900, 450, 24, menuExitColor);

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
                        menuShopColor = Color.WHITE;
                        Raylib.DrawRectangle(850, 400, 25, 25, Color.BLACK);
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            gameState = "shop";
                            menuTarget = 1;
                        }
                    }
                    else
                    {
                        menuShopColor = Color.BLACK;
                    }

                    if (menuTarget == 4)
                    {
                        menuExitColor = Color.WHITE;
                        Raylib.DrawRectangle(850, 450, 25, 25, Color.BLACK);
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
                if (gameState == "shop")
                {
                    //MENY LOGIK
                    (int mnTarget, string mngState, int mnStage) resultMenu = MenuTarget(menuTarget, gameState, stage);
                    menuTarget = resultMenu.mnTarget;
                    gameState = resultMenu.mngState;
                    stage = resultMenu.mnStage;

                    if (menuTarget == 1)
                    {
                        menuResumeColor = Color.BLACK;
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                        {
                            gameState = "level_" + level;
                        }
                    }
                    else
                    {
                        menuResumeColor = Color.GRAY;
                    }
                    if (menuTarget == 2)
                    {
                        speedImageSize = 0.6f;
                    }
                    else
                    {
                        speedImageSize = 0.5f;
                    }
                    if (menuTarget == 2 && speedBought == false)
                    {
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                        {
                            if (playerCoins >= 1)
                            {
                                //FRÅGA MICKE FÖR SPEED UPGRADES
                                playerSpeed = 0.8f;
                                playerCoins--;
                                speedBought = true;
                            }
                        }
                        menuExitColor = Color.BLACK;
                    }
                    else if (speedBought == true && menuTarget == 2)
                    {
                        menuExitColor = Color.LIME;
                    }
                    else if (speedBought == true)
                    {
                        menuExitColor = Color.GREEN;
                    }
                    else
                    {
                        menuExitColor = Color.GRAY;
                    }
                    if (menuTarget == 3 && skinBought == false)
                    {
                        menuOptionsColor = Color.BLACK;
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                        {
                            if (playerCoins >= 1)
                            {
                                //FRÅGA MICKE FÖR SPEED UPGRADES
                                playerCoins--;
                                skinBought = true;
                            }

                        }
                    }
                    else if (skinBought == true && menuTarget == 3)
                    {
                        menuOptionsColor = Color.LIME;
                    }
                    else if (skinBought == true)
                    {
                        menuOptionsColor = Color.GREEN;
                    }
                    else
                    {
                        menuOptionsColor = Color.GRAY;
                    }
                    //GRAFIK
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);
                    //TEXT
                    Raylib.DrawText("COINS: " + playerCoins, 200, 100, 32, Color.BLACK);
                    Raylib.DrawText("BACK", 200, 200, 32, menuResumeColor);
                    Raylib.DrawText("SPEED UPGRADE", 500, 200, 32, menuExitColor);
                    Raylib.DrawText("SKIN UPGRADE", 1000, 200, 32, menuOptionsColor);
                    //IMAGES
                    Raylib.DrawTextureEx(speedImage, new Vector2(520, 300), 0.0f, speedImageSize, Color.WHITE);
                    Raylib.EndDrawing();
                }
            }
        }
        static (float, float, string, float) PlayerMovement(float pX, float pY, string dir, float pSpeed)
        {

            //SPELAR RÖRELSE (hjälp)
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
            {
                pY -= pSpeed;
                dir = "w";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
            {
                pY += pSpeed;
                dir = "s";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                pX -= pSpeed;
                dir = "a";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                pX += pSpeed;
                dir = "d";
            }
            //DIAGONELL HASTIGHET KONTROLL
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                pY += pSpeed / 3;
                pX -= pSpeed / 3;
                dir = "dw";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                pY += pSpeed / 3;
                pX += pSpeed / 3;
                dir = "aw";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                pY -= pSpeed / 3;
                pX -= pSpeed / 3;
                dir = "ds";
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                pY -= pSpeed / 3;
                pX += pSpeed / 3;
                dir = "as";
            }

            return (pX, pY, dir, pSpeed);

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
            else if (gState == "level_three")
            {
                if (eY <= 75)
                {
                    eS = 2.2f;
                }
                else if (eY >= 925)
                {
                    eS = -2.2f;
                }
                eY += eS;
            }
            return (eY, eS, gState);
        }
        static (float, float, float, float, bool, string, int) EnemyCollision(float playX, float playY, float enemX, float enemY, bool pDead, string gState, int mStage)
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
            else if (gState == "level_three")
            {
                if (mStage == 1)
                {
                    if (enemX + 20 - playX <= 75 && enemX + 20 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 220 - playX <= 75 && enemX + 220 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 420 - playX <= 75 && enemX + 420 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 620 - playX <= 75 && enemX + 620 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 820 - playX <= 75 && enemX + 820 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 1020 - playX <= 75 && enemX + 1020 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                }
                else if (mStage == 2)
                {
                    if (enemX - 335 - playX <= 75 && enemX - 335 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX - 135 - playX <= 75 && enemX - 135 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 65 - playX <= 75 && enemX + 65 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 365 - playX <= 75 && enemX + 365 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 565 - playX <= 75 && enemX + 565 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 765 - playX <= 75 && enemX + 765 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX + 965 - playX <= 75 && enemX + 965 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }

                }
                else if (mStage == 3)
                {
                    if (enemX - 400 - playX <= 75 && enemX - 400 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX - 200 - playX <= 75 && enemX - 200 - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
                    {
                        pDead = true;
                    }
                    else if (enemX - playX <= 75 && enemX - playX >= -25 && enemY - playY <= 75 && enemY - playY >= -25)
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
            }
            return (playX, playY, enemX, enemY, pDead, gState, mStage);
        }
        static (int, string, int) MenuTarget(int mnTarget, string mngState, int mnStage)
        {
            if (mngState == "intro" || mngState == "pause")
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                {
                    if (mnTarget == 1)
                    {
                        mnTarget = 4;
                    }
                    else
                    {
                        mnTarget--;
                    }
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                {
                    if (mnTarget == 4)
                    {
                        mnTarget = 1;
                    }
                    else
                    {
                        mnTarget++;
                    }
                }
            }
            else if (mngState == "level_three" && mnStage == 0)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                {
                    if (mnTarget == 1)
                    {
                        mnTarget = 3;
                    }
                    else
                    {
                        mnTarget--;
                    }
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                {
                    if (mnTarget == 3)
                    {
                        mnTarget = 1;
                    }
                    else
                    {
                        mnTarget++;
                    }
                }
            }
            else if (mngState == "shop")
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                {
                    if (mnTarget == 1)
                    {
                        mnTarget = 3;
                    }
                    else
                    {
                        mnTarget--;
                    }
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                {
                    if (mnTarget == 3)
                    {
                        mnTarget = 1;
                    }
                    else
                    {
                        mnTarget++;
                    }
                }
            }
            return (mnTarget, mngState, mnStage);

        }
    }
}