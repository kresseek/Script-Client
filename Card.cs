using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card{
	//TODO : edit GambarDalam sesuai data
	public enum GambarDalam {bulat=0, kotak=1, segitiga=2, trapesium=3, oval=4, segienam=5};
	public enum Negara {hiu=0, pacah=1, jarum=2, kabek=3, tali=4, babi=5, sisiak=6, bengkok=7, suduang=8};

	public class Card{
		public int code { get; set; } 
		private bool isHiu { get; set; }
		public GambarDalam gambarDalam { get; set; }
		public Negara negara { get; set; }
		private int urutan { get; set; }

		public Card (int code, Negara negara, GambarDalam gambarDalam, int urutan)
		{
			this.code = code;
			//this.isHiu = isHiu;
			this.gambarDalam = gambarDalam;
			this.negara = negara;
			this.urutan = urutan;
		}
	}

}
