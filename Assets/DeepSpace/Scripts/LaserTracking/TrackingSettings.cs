using UnityEngine;

namespace DeepSpace.LaserTracking
{
	[System.Serializable]
	public class TrackingSettings
	{
		[SerializeField, Tooltip("Value in pixel.")]
		protected int screenWidthPixel = 1920;
		[SerializeField, Tooltip("Value in pixel.")]
		protected int screenHeightPixel = 1080;
		[SerializeField, Tooltip("Value in centimeter.")]
		protected float stageWidth = 1600f;
		[SerializeField, Tooltip("Value in centimeter.")]
		protected float stageHeight = 900f;

		public int ScreenWidthPixel
		{
			get { return this.screenWidthPixel; }
			set { this.screenWidthPixel = value; }
		}

		public int ScreenHeightPixel
		{
			get { return this.screenHeightPixel; }
			set { this.screenHeightPixel = value; }
		}

		public float StageWidth
		{
			get { return this.stageWidth; }
			set { this.stageWidth = value; }
		}

		public float StageHeight
		{
			get { return this.stageHeight; }
			set { this.stageHeight = value; }
		}

		public Vector2 GetScreenPositionFromRelativePosition(float x, float y)
		{
            Debug.Log(x * screenWidthPixel + ":X" + $"x:{x} * wid:{screenWidthPixel}");
			return new Vector2((int)Mathf.Round(x * screenWidthPixel), screenHeightPixel - (int)Mathf.Round(y * screenHeightPixel));
		}
	}
}