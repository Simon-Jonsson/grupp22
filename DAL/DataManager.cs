﻿using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DAL
{
    internal class DataManager
    {
        //Serializerar podcasts
        public void SerializePodcast(List<Podcast> podcastList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(podcastList.GetType());

            using (FileStream outFile = new FileStream("Podcasts.xml", FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(outFile, podcastList);
            }
        }

        //Deserializerar podcasts
        public List<Podcast> DeserializePodcast()
        {
            List<Podcast> listOfPodcastsToBeReturned;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Podcast>));

            using (FileStream inFile = new FileStream("Podcasts.xml", FileMode.Open, FileAccess.Read))
            {

                listOfPodcastsToBeReturned = (List<Podcast>)xmlSerializer.Deserialize(inFile);
            }
            return listOfPodcastsToBeReturned;
        }

        //Serializerar kategorier
        public void SerializeKategori(List<Kategori> kategoriList)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(kategoriList.GetType());

            using (FileStream outFile = new FileStream("Kategorier.xml", FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(outFile, kategoriList);
            }
        }

        //Deserializerar kategorier
        public List<Kategori> DeserializeKategori()
        {
            List<Kategori> listOfKategorierToBeReturned;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Kategori>));

            using (FileStream inFile = new FileStream("Kategorier.xml", FileMode.Open, FileAccess.Read))
            {

                listOfKategorierToBeReturned = (List<Kategori>)xmlSerializer.Deserialize(inFile);
            }
            return listOfKategorierToBeReturned;
        }

    }
}
