﻿using System.Collections.Generic;
using System.Drawing;

namespace Monopoly
{
    public class Board : List<Square>
    {
        public Board()
        : base(40)
        {
        }

        public static Board StandardBoard()
        {
            var chance = new ChanceSquare("Sorte ou Revés");
            return new Board
            {
                new StartLand(),
                new Land("Leblon", 100, Color.Pink)
                {
                    EnhancementPrice = 50, RentPrice = new RentPrice(6, 30, 90, 270, 400, 500)
                },
                chance,
                new Land("Av. Presidente Vargas", 60, Color.Pink)
                {
                    EnhancementPrice = 50, RentPrice = new RentPrice(2, 10, 30, 90, 160, 250)
                },
                new Land("Av. Nossa Senhora de Copacabana", 60, Color.Pink)
                {
                    EnhancementPrice = 50, RentPrice = new RentPrice(4, 20, 60, 180, 320, 450)
                },
                new Company("Companhia Ferroviária", 200, 50),
                new Land("Av. Brigadeiro Faria Lima", 240, Color.Blue)
                {
                    EnhancementPrice = 150, RentPrice = new RentPrice(20, 100, 300, 750, 925, 1100)
                },
                new Land("Av. Rebolsas", 220, Color.Blue)
                {
                    EnhancementPrice = 150, RentPrice = new RentPrice(18, 90, 250, 700, 875, 1050)
                },
                new Company("Companhia de Viação", 200, 50),
                new Land("Av. 9 de Julho", 220, Color.Blue)
                {
                    EnhancementPrice = 150, RentPrice = new RentPrice(18, 90, 250, 700, 875, 1050)
                },
                new FreezeVisit(),
                new Land("Av. Europa", 200, Color.Purple)
                {
                    EnhancementPrice = 150, RentPrice = new RentPrice(16, 80, 220, 600, 800, 1000)
                },
                chance,
                new Land("Rua Augusta", 180, Color.Purple)
                {
                    EnhancementPrice = 100, RentPrice = new RentPrice(14, 70, 200, 550, 750, 950)
                },
                new Land("Av. Pacaembú", 180, Color.Purple)
                {
                    EnhancementPrice = 100, RentPrice = new RentPrice(14, 70, 200, 550, 750, 950)
                },
                new Company("Companhia de Taxi", 200, 50),
                chance,
                new Land("Interlagos", 350, Color.Orange)
                {
                    EnhancementPrice = 200, RentPrice = new RentPrice(35, 175, 500, 1100, 1300, 1500)
                },
                new CommunityChest(),
                new Land("Morumbi", 400, Color.Orange)
                {
                    EnhancementPrice = 200, RentPrice = new RentPrice(50, 200, 600, 1400, 1700, 2000)
                },
                new FreeStop(),
                new Land("Flamengo", 120, Color.Red)
                {
                    EnhancementPrice = 50, RentPrice = new RentPrice(8, 40, 100, 300, 450, 600)
                },
                chance,
                new Land("Botafogo", 100, Color.Red)
                {
                    EnhancementPrice = 50, RentPrice = new RentPrice(6, 30, 90, 270, 400, 500)
                },
                new Theft(),
                new Company("Companhia de Nevegação", 200, 50),
                new Land("Av. Brasil", 160, Color.Yellow)
                {
                    EnhancementPrice = 100, RentPrice = new RentPrice(12, 60, 180, 500, 700, 900)
                },
                chance,
                new Land("Av. Paulista", 160, Color.Yellow)
                {
                    EnhancementPrice = 100, RentPrice = new RentPrice(10, 50, 150, 450, 625, 750)
                },
                new Land("Av. Jardim Europa", 140, Color.Yellow)
                {
                    EnhancementPrice = 100, RentPrice = new RentPrice(10, 50, 150, 450, 625, 750)
                },
                new FreezeEntry(),
                new Land("Copacabana", 260, Color.Green)
                {
                    EnhancementPrice = 150, RentPrice = new RentPrice(22, 110, 330, 800, 975, 1150)
                },
                new Company("Companhia de Aviação", 200, 50),
                new Land("Av. Vieira Souto", 320, Color.Green)
                {
                    EnhancementPrice = 200, RentPrice = new RentPrice(28, 150, 450, 1000, 1200, 1400)
                },
                new Land("Av. Atlântica", 300, Color.Green)
                {
                    EnhancementPrice = 200, RentPrice = new RentPrice(26, 130, 390, 900, 1100, 1275)
                },
                new Company("Companhia de Taxi Aéreo", 200, 50),
                new Land("Ipanema", 300, Color.Green)
                {
                    EnhancementPrice = 200, RentPrice = new RentPrice(26, 130, 390, 900, 1100, 1275)
                },
                chance,
                new Land("Jardim Paulista", 260, Color.CornflowerBlue)
                {
                    EnhancementPrice = 150, RentPrice = new RentPrice(24, 120, 360, 850, 1025, 1200)
                },
                new Land("Broklin", 260, Color.CornflowerBlue)
                {
                    EnhancementPrice = 150, RentPrice = new RentPrice(22, 110, 330, 800, 975, 1150)
                }
            };
        }
    }
}