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
        public static Texture2D exp;
        public static SpriteFont debug;
        private static Texture2D laser;
        public static Texture2D x200x177;
        public static Texture2D bul;
        public static Texture2D ball;
        public static Texture2D ball2;
        public static Texture2D ball150;
        public static Texture2D ball280;
        public static Texture2D _1x1;

        //zkusit pak všechny udělat takhle
        //public static Texture2D tex { get; private set;  }

        //public static Texture2D playerKnifeAttack;

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

            //ty ve final version jsou už fakt final version
            playerRifleIdle = manager.Load<Texture2D>("RifleFolder\\rifle_idle");
            playerRifleMeleeattack = manager.Load<Texture2D>("RifleFolder\\rifle_meleeattack");
            playerRifleMove = manager.Load<Texture2D>("RifleFolder\\rifle_move");
            playerRifleReload = manager.Load<Texture2D>("RifleFolder\\rifle_reload");
            playerRifleShoot = manager.Load<Texture2D>("RifleFolder\\rifle_shoot");


            //fixnout shake idlu a movu!!!
            playerShotgunIdle = manager.Load<Texture2D>("Textures\\shotgunIdle");
            playerShotgunMeleeattack = manager.Load<Texture2D>("Textures\\shotgunMeleeattack");
            //dodělat
            //playerShotgunMove = manager.Load<Texture2D>("Textures\\shotgunMove");
            playerShotgunMove = manager.Load<Texture2D>("shotgunMove2");
            //
            playerShotgunReload = manager.Load<Texture2D>("Textures\\shotgunReload");
            playerShotgunShoot = manager.Load<Texture2D>("Textures\\shotgunShoot");

            //ty z předěláno
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

            //playerKnifeAttack = manager.Load<Texture2D>("Textures\\melee_attack3");
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
