using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace TankGame
{
    class Client
    {
        private TcpClient client;
        private string ip = "127.0.0.1";

        private int portIn = 6000;   //port use to connect
        private int portOut = 7000;  //port to recieve

        private string data;
        public Thread thread;

        public Board board;
        public Client(){

            thread = new Thread(new ThreadStart(receive));
            this.board = new Board();

        }

        //to send message to the server
        public void send(string message)
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), portIn);
            Stream stream = client.GetStream();

            ASCIIEncoding asciiencode = new ASCIIEncoding();
            byte[] msg = asciiencode.GetBytes(message);
            stream.Write(msg, 0, msg.Length);
            stream.Close();
            client.Close();

            if (message.Equals("JOIN#"))
            {    //starts the game with the command JOIN#
                thread.Start();
            }
        }
        //to get messages from server
        public void receive()
        {
            TcpListener listner = new TcpListener(IPAddress.Parse(ip), portOut);
            while (true)
            {
                listner.Start();
                TcpClient clientRecieve = listner.AcceptTcpClient();
                Stream streamRecieve = clientRecieve.GetStream();
                Byte[] bytes = new Byte[256];

                int i;
                data = null;

                while ((i = streamRecieve.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                }

                formatMsg(data);
                streamRecieve.Close();
                listner.Stop();
                clientRecieve.Close();
            }
        }

        public void formatMsg(String msg)
        {
           
            String[] firstpart = msg.Split('#');
            String[] parts = firstpart[0].Split(':');

            try
            {
                String msg_format = parts[0];
                if (msg_format.Equals("I")) // game instant received
                {
                    Console.WriteLine("================================================================================\n");
                    Console.WriteLine("New Game Instant received");
                    String player = parts[1];
                    Console.WriteLine("Player: " + player);


                    for (int i = 0; i < 5; i++)
                    {
                        board.players[i] = new Player("P" + i);
                    }

                    // reading brick position details
                    String brick_map = parts[2];
                    String[] bricks = brick_map.Split(';');
                    int brick_no = 1;
                    foreach (String brick in bricks)
                    {
                        String[] brick_location = brick.Split(',');
                        Brick bb = new Brick();
                        bb.setNo(brick_no);
                        bb.setPostion(int.Parse(brick_location[0]), int.Parse(brick_location[1]));
                        board.blocks[int.Parse(brick_location[0]), int.Parse(brick_location[1])] = bb;
                        brick_no++;
                    }

                    // end reading brick posiotion details


                    // reading stone position details
                    String stone_map = parts[3];
                    String[] stones = stone_map.Split(';');
                    int stone_no = 1;
                    foreach (String stone in stones)
                    {
                        String[] stone_location = stone.Split(',');
                        Stone ss = new Stone();
                        ss.setNo(stone_no);
                        ss.setPostion(int.Parse(stone_location[0]), int.Parse(stone_location[1]));
                        board.blocks[int.Parse(stone_location[0]), int.Parse(stone_location[1])] = ss;
                        stone_no++;
                    }

                    // end of reading stone position details


                    // reading water position details
                    String water_map = parts[4];
                    String[] waters = water_map.Split(';');
                    int water_no = 1;
                    foreach (String water in waters)
                    {
                        String[] water_location = water.Split(',');
                        Water ww = new Water();
                        ww.setNo(water_no);
                        ww.setPostion(int.Parse(water_location[0]), int.Parse(water_location[1]));
                        board.blocks[int.Parse(water_location[0]), int.Parse(water_location[1])] = ww;
                        water_no++;
                    }

                    // end of reading water position details

                }
                else if (msg_format.Equals("G")) // global update received
                {

                    int player_no = 1;
                    for (player_no = 1; player_no <= 5; player_no++)
                    {

                        String player_code = parts[player_no];
                        if (player_code.Substring(0, 1).Equals("P")) // this is a player sub string
                        {
                            String[] player_details = player_code.Split(';');
                            String num = Convert.ToString(player_details[0][1]);
                            int p_id = int.Parse(num);
                            String[] player_log = player_details[1].Split(',');
                            board.players[p_id].setPostion(int.Parse(player_log[0]), int.Parse(player_log[1]));
                            board.blocks[int.Parse(player_log[0]), int.Parse(player_log[1])] = board.players[p_id];
                            board.players[p_id].setDirection(int.Parse(player_details[2]));
                            board.players[p_id].setLife(int.Parse(player_details[3]));
                            board.players[p_id].setHealth(int.Parse(player_details[4]));
                            board.players[p_id].setCoins(int.Parse(player_details[5]));
                            board.players[p_id].setPoints(int.Parse(player_details[6]));

                        }
                        else
                            break;
                    }
                    Console.WriteLine("================================================================================\n");
                    Console.WriteLine("Moving shot details");
                    String[] shots = parts[player_no].Split(';');
                    foreach (String shot in shots)
                    {
                        String[] shot_details = shot.Split(',');
                        Brick b = (Brick)board.blocks[int.Parse(shot_details[0]), int.Parse(shot_details[1])];
                        b.setDamage(int.Parse(shot_details[2]));
                        board.blocks[int.Parse(shot_details[0]), int.Parse(shot_details[1])] = b;
                        Console.WriteLine("Shot details ####  x==> " + shot_details[0] + " y ==> " + shot_details[1] + " damage level ==> " + shot_details[2]);
                    }

                }
                else if (msg_format.Equals("C")) // coin detail received
                {
                    Coinpack cp = new Coinpack();
                    String[] location = parts[1].Split(',');
                    cp.setPostion(int.Parse(location[0]), int.Parse(location[1]));
                    cp.setTime(int.Parse(parts[2]));
                    cp.setAmount(int.Parse(parts[3]));
                    board.blocks[int.Parse(location[0]), int.Parse(location[1])] = cp;
                }

                else if (msg_format.Equals("L")) // lifepack detail received
                {
                    Lifepack lp = new Lifepack();
                    String[] location = parts[1].Split(',');
                    lp.setPostion(int.Parse(location[0]), int.Parse(location[1]));
                    lp.setTime(int.Parse(parts[2]));
                    board.blocks[int.Parse(location[0]), int.Parse(location[1])] = lp;
                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void checkTTL()
        {
            int time;
            foreach (Block b in board.blocks)
            {
                if(b is Lifepack )//|| b is Coinpack)
                time=(Lifepack)b.;
                lifepack_time -= 1;
                lp.setTime(lifepack_time);
                if (lifepack_time == 0)
                {
                    Sand sand = new Sand();
                    
                }
            }
        }
    }
}
