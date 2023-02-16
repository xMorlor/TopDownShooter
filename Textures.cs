using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace TopDownShooterFinal
{
    static class Textures
    {
        private static Texture2D playerKnifeIdle;
        private static Texture2D playerKnifeMeleeattack;
        private static Texture2D playerKnifeMove;

        private static Texture2D playerHandgunIdle;
        private static Texture2D playerHandgunMeleeattack;
        private static Texture2D playerHandgunMove;
        private static Texture2D playerHandgunReload;
        private static Texture2D playerHandgunShoot;

        private static Texture2D playerRifleIdle;
        private static Texture2D playerRifleMeleeattack;
        private static Texture2D playerRifleMove;
        private static Texture2D playerRifleReload;
        private static Texture2D playerRifleShoot;

        private static Texture2D playerShotgunIdle;
        private static Texture2D playerShotgunMeleeattack;
        private static Texture2D playerShotgunMove;
        private static Texture2D playerShotgunReload;
        private static Texture2D playerShotgunShoot;

        private static Texture2D zombieAttack1;
        private static Texture2D zombieAttack2;
        private static Texture2D zombieAttack3;
        private static Texture2D zombieDeath1;
        private static Texture2D zombieDeath2;
        private static Texture2D zombieEating;
        private static Texture2D zombieIdle;
        private static Texture2D zombieRun;
        private static Texture2D zombieSaunter;
        private static Texture2D zombieWalk;

        //tracks
        private static Texture2D run0;
        private static Texture2D run1;
        private static Texture2D run2;
        private static Texture2D run3;
        private static Texture2D run4;
        private static Texture2D run5;
        private static Texture2D run6;
        private static Texture2D run7;
        private static Texture2D run8;
        private static Texture2D run9;
        private static Texture2D run10;
        private static Texture2D run11;
        private static Texture2D run12;
        private static Texture2D run13;
        private static Texture2D run14;
        private static Texture2D run15;
        private static Texture2D run16;
        private static Texture2D run17;
        private static Texture2D run18;
        private static Texture2D run19;

        private static Texture2D akLogo;
        private static Texture2D shotgunLogo;
        private static Texture2D handgunLogo;
        private static Texture2D knifeLogo;

        private static Texture2D bloodSmall;
        private static Texture2D bloodMedium;
        private static Texture2D bloodLarge;

        private static Texture2D muzzleFlash1;
        private static Texture2D muzzleFlash2;
        private static Texture2D muzzleFlash3;

        //debug
        public static Texture2D exp { get; private set; }
        public static SpriteFont debug { get; private set; }
        private static Texture2D laser;
        public static Texture2D x200x177 { get; private set; }
        public static Texture2D bul { get; private set; }
        public static Texture2D ball { get; private set; }
        public static Texture2D ball2 { get; private set; }
        public static Texture2D ball150 { get; private set; }
        public static Texture2D ball280 { get; private set; }
        public static Texture2D _1x1 { get; private set; }

        public static Texture2D testTile { get; private set; }
        public static Texture2D tTile1 { get; private set; }
        public static Texture2D tTile2 { get; private set; }
        public static Texture2D tTile3 { get; private set; }
        public static Texture2D tTile4 { get; private set; }
        public static Texture2D tTile5 { get; private set; }
        public static Texture2D tTile6 { get; private set; }

        public static Texture2D groundTile1 { get; private set; }
        public static Texture2D groundTile2 { get; private set; }
        public static Texture2D groundTile3 { get; private set; }
        public static Texture2D groundTile4 { get; private set; }
        public static Texture2D groundTile5 { get; private set; }
        public static Texture2D groundTile6 { get; private set; }
        public static Texture2D groundTile7 { get; private set; }
        public static Texture2D groundTile8 { get; private set; }
        public static Texture2D groundTile9 { get; private set; }

        public static Texture2D grass2 { get; private set; }
        public static Texture2D grass3 { get; private set; }
        public static Texture2D grass4 { get; private set; }
        public static Texture2D grass5 { get; private set; }
        public static Texture2D grass6 { get; private set; }
        public static Texture2D grass7 { get; private set; }
        public static Texture2D grass8 { get; private set; }
        public static Texture2D grass9 { get; private set; }
        public static Texture2D grassFull { get; private set; }


        public static Texture2D waterTile1 { get; private set; }

        public static Texture2D groundCrack1 { get; private set; }
        public static Texture2D groundCrack2 { get; private set; }
        public static Texture2D groundCrack3 { get; private set; }
        public static Texture2D groundCrack4 { get; private set; }
        public static Texture2D groundCrack5 { get; private set; }
        public static Texture2D groundCrack6 { get; private set; }
        public static Texture2D groundCrack7 { get; private set; }
        public static Texture2D groundCrack8 { get; private set; }
        public static Texture2D groundCrack9 { get; private set; }
        public static Texture2D groundCrack10 { get; private set; }
        public static Texture2D groundCrack11 { get; private set; }
        public static Texture2D groundCrack12 { get; private set; }
        public static Texture2D groundCrack13 { get; private set; }
        public static Texture2D groundCrack14 { get; private set; }
        public static Texture2D groundCrack15 { get; private set; }
        public static Texture2D groundCrack16 { get; private set; }
        public static Texture2D groundCrack17 { get; private set; }
        public static Texture2D groundCrack18 { get; private set; }
        public static Texture2D groundCrack19 { get; private set; }
        public static Texture2D groundCrack20 { get; private set; }
        public static Texture2D groundCrack21 { get; private set; }
        public static Texture2D groundCrack22 { get; private set; }
        public static Texture2D groundCrack23 { get; private set; }
        public static Texture2D groundCrack24 { get; private set; }
        public static Texture2D groundCrack25 { get; private set; }
        public static Texture2D groundCrack26 { get; private set; }
        public static Texture2D groundCrack27 { get; private set; }
        public static Texture2D groundCrack28 { get; private set; }
        public static Texture2D groundCrack29 { get; private set; }
        public static Texture2D groundCrack30 { get; private set; }
        public static Texture2D groundCrack31 { get; private set; }
        public static Texture2D groundCrack32 { get; private set; }
        public static Texture2D groundCrack33 { get; private set; }

        public static Texture2D wall1_139 { get; private set; }
        public static Texture2D wall2_139 { get; private set; }
        public static Texture2D wall3_139 { get; private set; }
        public static Texture2D wall4_139 { get; private set; }
        public static Texture2D wall5_139 { get; private set; }
        public static Texture2D wall6_139 { get; private set; }

        public static void Load(ContentManager manager)
        {
            playerKnifeIdle = manager.Load<Texture2D>("Textures\\knife_idle");
            playerKnifeMeleeattack = manager.Load<Texture2D>("Textures\\knife_melee_attack");
            playerKnifeMove = manager.Load<Texture2D>("Textures\\knife_move");

            playerHandgunIdle = manager.Load<Texture2D>("Textures\\handgun_idle");
            playerHandgunMeleeattack = manager.Load<Texture2D>("Textures\\handgun_melee_attack");
            playerHandgunMove = manager.Load<Texture2D>("Textures\\handgun_move");
            playerHandgunReload = manager.Load<Texture2D>("Textures\\handgun_reload");
            playerHandgunShoot = manager.Load<Texture2D>("Textures\\handgun_shoot");
            playerRifleIdle = manager.Load<Texture2D>("RifleFolder\\rifle_idle");
            playerRifleMeleeattack = manager.Load<Texture2D>("RifleFolder\\rifle_meleeattack");
            playerRifleMove = manager.Load<Texture2D>("RifleFolder\\rifle_move");
            playerRifleReload = manager.Load<Texture2D>("RifleFolder\\rifle_reload");
            playerRifleShoot = manager.Load<Texture2D>("RifleFolder\\rifle_shoot");

            playerShotgunIdle = manager.Load<Texture2D>("Textures\\shotgunIdle");
            playerShotgunMeleeattack = manager.Load<Texture2D>("Textures\\shotgunMeleeattack");
            playerShotgunMove = manager.Load<Texture2D>("shotgunMove2");
            playerShotgunReload = manager.Load<Texture2D>("Textures\\shotgunReload");
            playerShotgunShoot = manager.Load<Texture2D>("Textures\\shotgunShoot");

            zombieAttack1 = manager.Load<Texture2D>("Zombie\\attack1");
            zombieAttack2 = manager.Load<Texture2D>("Zombie\\attack2");
            zombieAttack3 = manager.Load<Texture2D>("Zombie\\attack3");
            zombieDeath1 = manager.Load<Texture2D>("Zombie\\death1");
            zombieDeath2 = manager.Load<Texture2D>("Zombie\\death2");
            zombieEating = manager.Load<Texture2D>("Zombie\\eating");
            zombieIdle = manager.Load<Texture2D>("Zombie\\idle");
            zombieRun = manager.Load<Texture2D>("Zombie\\run");
            zombieSaunter = manager.Load<Texture2D>("Zombie\\saunter");
            zombieWalk = manager.Load<Texture2D>("Zombie\\walk");

            run0 = manager.Load<Texture2D>("tracks\\run0");
            run1 = manager.Load<Texture2D>("tracks\\run1");
            run2 = manager.Load<Texture2D>("tracks\\run2");
            run3 = manager.Load<Texture2D>("tracks\\run3");
            run4 = manager.Load<Texture2D>("tracks\\run4");
            run5 = manager.Load<Texture2D>("tracks\\run5");
            run6 = manager.Load<Texture2D>("tracks\\run6");
            run7 = manager.Load<Texture2D>("tracks\\run7");
            run8 = manager.Load<Texture2D>("tracks\\run8");
            run9 = manager.Load<Texture2D>("tracks\\run9");
            run10 = manager.Load<Texture2D>("tracks\\run10");
            run11 = manager.Load<Texture2D>("tracks\\run11");
            run12 = manager.Load<Texture2D>("tracks\\run12");
            run13 = manager.Load<Texture2D>("tracks\\run13");
            run14 = manager.Load<Texture2D>("tracks\\run14");
            run15 = manager.Load<Texture2D>("tracks\\run15");
            run16 = manager.Load<Texture2D>("tracks\\run16");
            run17 = manager.Load<Texture2D>("tracks\\run17");
            run18 = manager.Load<Texture2D>("tracks\\run18");
            run19 = manager.Load<Texture2D>("tracks\\run19");

            akLogo = manager.Load<Texture2D>("Logos\\akLogo1");
            shotgunLogo = manager.Load<Texture2D>("Logos\\shotgunLogo1");
            handgunLogo = manager.Load<Texture2D>("Logos\\handgunLogo1");
            knifeLogo = manager.Load<Texture2D>("Logos\\knifeLogo1");

            bloodSmall = manager.Load<Texture2D>("Textures\\bloodSmall");
            bloodMedium = manager.Load<Texture2D>("Textures\\bloodMedium");
            bloodLarge = manager.Load<Texture2D>("Textures\\bloodLarge");

            muzzleFlash1 = manager.Load<Texture2D>("Textures\\muzzleFlash1");
            muzzleFlash2 = manager.Load<Texture2D>("Textures\\muzzleFlash2");
            muzzleFlash3 = manager.Load<Texture2D>("Textures\\muzzleFlash3");

            laser = manager.Load<Texture2D>("Textures\\laserReal");
            exp = manager.Load<Texture2D>("Textures\\experience");
            debug = manager.Load<SpriteFont>("Fonts\\debug");
            x200x177 = manager.Load<Texture2D>("Textures\\200x177");
            bul = manager.Load<Texture2D>("image");//bullet
            ball = manager.Load<Texture2D>("Textures\\ball");
            ball2 = manager.Load<Texture2D>("Textures\\ball2");
            ball150 = manager.Load<Texture2D>("ball3");
            ball280 = manager.Load<Texture2D>("ball4");
            _1x1 = manager.Load<Texture2D>("1x1pixels");

            testTile = manager.Load<Texture2D>("testTile");

            tTile1 = manager.Load<Texture2D>("test\\1\\testtile1");
            tTile2 = manager.Load<Texture2D>("test\\1\\testtile2");
            tTile3 = manager.Load<Texture2D>("test\\1\\testtile3");
            tTile4 = manager.Load<Texture2D>("test\\1\\testtile4");
            tTile5 = manager.Load<Texture2D>("test\\1\\testtile5");
            tTile6 = manager.Load<Texture2D>("test\\1\\testtile6");

            groundTile1 = manager.Load<Texture2D>("Tiles\\testTile");
            groundTile2 = manager.Load<Texture2D>("Tiles\\ground2");
            groundTile3 = manager.Load<Texture2D>("Tiles\\ground3");
            groundTile4 = manager.Load<Texture2D>("Tiles\\ground4");
            groundTile5 = manager.Load<Texture2D>("Tiles\\ground5");
            groundTile6 = manager.Load<Texture2D>("Tiles\\ground6");
            groundTile7 = manager.Load<Texture2D>("Tiles\\ground7");
            groundTile8 = manager.Load<Texture2D>("Tiles\\ground8");
            groundTile9 = manager.Load<Texture2D>("Tiles\\ground9");

            grassFull = manager.Load<Texture2D>("Tiles\\grassFull");
            grass2 = manager.Load<Texture2D>("Tiles\\grass2");
            grass3 = manager.Load<Texture2D>("Tiles\\grass3");
            grass4 = manager.Load<Texture2D>("Tiles\\grass4");
            grass5 = manager.Load<Texture2D>("Tiles\\grass5");
            grass6 = manager.Load<Texture2D>("Tiles\\grass6");
            grass7 = manager.Load<Texture2D>("Tiles\\grass7");
            grass8 = manager.Load<Texture2D>("Tiles\\grass8");
            grass9 = manager.Load<Texture2D>("Tiles\\grass9");

            waterTile1 = manager.Load<Texture2D>("Tiles\\water1");

            groundCrack1 = manager.Load<Texture2D>("GroundCracks\\1");
            groundCrack2 = manager.Load<Texture2D>("GroundCracks\\2");
            groundCrack3 = manager.Load<Texture2D>("GroundCracks\\3");
            groundCrack4 = manager.Load<Texture2D>("GroundCracks\\4");
            groundCrack5 = manager.Load<Texture2D>("GroundCracks\\5");
            groundCrack6 = manager.Load<Texture2D>("GroundCracks\\6");
            groundCrack7 = manager.Load<Texture2D>("GroundCracks\\7");
            groundCrack8 = manager.Load<Texture2D>("GroundCracks\\8");
            groundCrack9 = manager.Load<Texture2D>("GroundCracks\\9");
            groundCrack10 = manager.Load<Texture2D>("GroundCracks\\10");
            groundCrack11 = manager.Load<Texture2D>("GroundCracks\\11");
            groundCrack12 = manager.Load<Texture2D>("GroundCracks\\12");
            groundCrack13 = manager.Load<Texture2D>("GroundCracks\\13");
            groundCrack14 = manager.Load<Texture2D>("GroundCracks\\14");
            groundCrack15 = manager.Load<Texture2D>("GroundCracks\\15");
            groundCrack16 = manager.Load<Texture2D>("GroundCracks\\16");
            groundCrack17 = manager.Load<Texture2D>("GroundCracks\\17");
            groundCrack18 = manager.Load<Texture2D>("GroundCracks\\18");
            groundCrack19 = manager.Load<Texture2D>("GroundCracks\\19");
            groundCrack20 = manager.Load<Texture2D>("GroundCracks\\20");
            groundCrack21 = manager.Load<Texture2D>("GroundCracks\\21");
            groundCrack22 = manager.Load<Texture2D>("GroundCracks\\22");
            groundCrack23 = manager.Load<Texture2D>("GroundCracks\\23");
            groundCrack24 = manager.Load<Texture2D>("GroundCracks\\24");
            groundCrack25 = manager.Load<Texture2D>("GroundCracks\\25");
            groundCrack26 = manager.Load<Texture2D>("GroundCracks\\26");
            groundCrack27 = manager.Load<Texture2D>("GroundCracks\\27");
            groundCrack28 = manager.Load<Texture2D>("GroundCracks\\28");
            groundCrack29 = manager.Load<Texture2D>("GroundCracks\\29");
            groundCrack30 = manager.Load<Texture2D>("GroundCracks\\30");
            groundCrack31 = manager.Load<Texture2D>("GroundCracks\\31");
            groundCrack32 = manager.Load<Texture2D>("GroundCracks\\32");
            groundCrack33 = manager.Load<Texture2D>("GroundCracks\\33");

            wall1_139 = manager.Load<Texture2D>("test2_139\\testtile1");
            wall2_139 = manager.Load<Texture2D>("test2_139\\testtile2");
            wall3_139 = manager.Load<Texture2D>("test2_139\\testtile3");
            wall4_139 = manager.Load<Texture2D>("test2_139\\testtile4");
            wall5_139 = manager.Load<Texture2D>("test2_139\\testtile5");
            wall6_139 = manager.Load<Texture2D>("test2_139\\testtile6");
        }

        public static Texture2D PlayerKnifeIdle { get { return playerKnifeIdle; } }
        public static Texture2D PlayerKnifeMeleeattack { get { return playerKnifeMeleeattack; } }
        public static Texture2D PlayerKnifeMove { get { return playerKnifeMove; } }

        public static Texture2D PlayerHandgunIdle { get { return playerHandgunIdle; } }
        public static Texture2D PlayerHandgunMeleeattack { get { return playerHandgunMeleeattack; } }
        public static Texture2D PlayerHandgunMove { get { return playerHandgunMove; } }
        public static Texture2D PlayerHandgunReload { get { return playerHandgunReload; } }
        public static Texture2D PlayerHandgunShoot { get { return playerHandgunShoot; } }

        public static Texture2D PlayerRifleIdle { get { return playerRifleIdle; } }
        public static Texture2D PlayerRifleMeleeattack { get { return playerRifleMeleeattack; } }
        public static Texture2D PlayerRifleMove { get { return playerRifleMove; } }
        public static Texture2D PlayerRifleReload { get { return playerRifleReload; } }
        public static Texture2D PlayerRifleShoot { get { return playerRifleShoot; } }

        public static Texture2D PlayerShotgunIdle { get { return playerShotgunIdle; } }
        public static Texture2D PlayerShotgunMeleeattack { get { return playerShotgunMeleeattack; } }
        public static Texture2D PlayerShotgunMove { get { return playerShotgunMove; } }
        public static Texture2D PlayerShotgunReload { get { return playerShotgunReload; } }
        public static Texture2D PlayerShotgunShoot { get { return playerShotgunShoot; } }

        public static Texture2D ZombieAttack1 { get { return zombieAttack1; } }
        public static Texture2D ZombieAttack2 { get { return zombieAttack2; } }
        public static Texture2D ZombieAttack3 { get { return zombieAttack3; } }
        public static Texture2D ZombieDeath1 { get { return zombieDeath1; } }
        public static Texture2D ZombieDeath2 { get { return zombieDeath2; } }
        public static Texture2D ZombieEating { get { return zombieEating; } }
        public static Texture2D ZombieIdle { get { return zombieIdle; } }
        public static Texture2D ZombieRun { get { return zombieRun; } }
        public static Texture2D ZombieSaunter { get { return zombieSaunter; } }
        public static Texture2D ZombieWalk { get { return zombieWalk; } }

        public static Texture2D Run0 { get { return run0; } }
        public static Texture2D Run1 { get { return run1; } }
        public static Texture2D Run2 { get { return run2; } }
        public static Texture2D Run3 { get { return run3; } }
        public static Texture2D Run4 { get { return run4; } }
        public static Texture2D Run5 { get { return run5; } }
        public static Texture2D Run6 { get { return run6; } }
        public static Texture2D Run7 { get { return run7; } }
        public static Texture2D Run8 { get { return run8; } }
        public static Texture2D Run9 { get { return run9; } }
        public static Texture2D Run10 { get { return run10; } }
        public static Texture2D Run11 { get { return run11; } }
        public static Texture2D Run12 { get { return run12; } }
        public static Texture2D Run13 { get { return run13; } }
        public static Texture2D Run14 { get { return run14; } }
        public static Texture2D Run15 { get { return run15; } }
        public static Texture2D Run16 { get { return run16; } }
        public static Texture2D Run17 { get { return run17; } }
        public static Texture2D Run18 { get { return run18; } }
        public static Texture2D Run19 { get { return run19; } }

        public static Texture2D AkLogo { get { return akLogo; } }
        public static Texture2D ShotgunLogo { get { return shotgunLogo; } }
        public static Texture2D HandgunLogo { get { return handgunLogo; } }
        public static Texture2D KnifeLogo { get { return knifeLogo; } }

        public static Texture2D BloodSmall { get { return bloodSmall; } }
        public static Texture2D BloodMedium { get { return bloodMedium; } }
        public static Texture2D BloodLarge { get { return bloodLarge; } }

        public static Texture2D MuzzleFlash1 { get { return muzzleFlash1; } }
        public static Texture2D MuzzleFlash2 { get { return muzzleFlash2; } }
        public static Texture2D MuzzleFlash3 { get { return muzzleFlash3; } }

        public static Texture2D Laser { get { return laser; } }
    }
}
