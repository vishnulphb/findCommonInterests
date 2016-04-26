using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace findCommonInterests
{
    class Program
    {
        static void Main(string[] args)
        {
            findCommonInterestsBetweenNonFriends("interests.txt");
        }
        public static int findOverlap(string word1, string word2)
        {
            string[] words1 = word1.Split('|');
            string[] words2 = word2.Split('|');
            var overlap = words1.Intersect(words2);

            int common = overlap.ToArray().Length;
            return common;
        }
        public static Dictionary<string, string> createInterestsList(string inputFile)
        {
            StreamReader sr = new StreamReader(inputFile);
            StreamWriter sw = new StreamWriter("InterestList.csv");
            //List<string> friendList = new List<string>();
            //Dictionary<string, string> friendList = new Dictionary<string, string>();
            string header = sr.ReadLine();
            sw.WriteLine(header);
            string line = null;
            Dictionary<string, string> friendsListDict = new Dictionary<string, string>();

            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                string friend = words[0];
                string friendNumber = words[1];
                if (!friendsListDict.ContainsKey(friend))
                {
                    friendsListDict.Add(friend, friendNumber);
                }
                else
                {
                    string existingFriends = friendsListDict[friend];
                    friendsListDict[friend] = existingFriends + "|" + friendNumber;
                }
            }
            foreach (KeyValuePair<string, string> pair in friendsListDict)
            {
                sw.WriteLine(pair.Key + "," + pair.Value);

            }

            sw.Close();
            return friendsListDict;
        }

        private static Dictionary<string, string> findCommonInterestsBetweenNonFriends(string inputPath)
        {
            Dictionary<string, string> comp1 = createInterestsList(inputPath);
            Dictionary<string, string> comp2 = comp1;
            Dictionary<string, string> output = new Dictionary<string, string>();
            StreamWriter sw = new StreamWriter("finalOutput_Interests.csv");

            sw.WriteLine("FriendShip Between" + "," + "Common Interests");
            foreach (KeyValuePair<string, string> pair in comp1)
            {
                foreach (KeyValuePair<string, string> pair2 in comp2)
                {
                    if (findOverlap(pair.Value, pair2.Key) == 0 && pair.Key != pair2.Key)
                    {
                        //key
                        string possibleFriendship = pair.Key + "+" + pair2.Key;
                        //value
                        int numOfCommonFriends = findOverlap(pair.Value, pair2.Value);
                        string commonFriends = numOfCommonFriends.ToString();

                        if (Convert.ToInt16(numOfCommonFriends) > 0)
                        {
                            output.Add(possibleFriendship, commonFriends);
                            sw.WriteLine(possibleFriendship + "," + commonFriends);
                        }
                    }
                }
            }
            sw.Close();
            return output;
        }
    }
}
