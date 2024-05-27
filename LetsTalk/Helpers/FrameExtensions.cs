using Windows.UI.Xaml.Controls;

namespace LetsTalk.Helpers
{
    public static class FrameExtensions
    {

        /// <summary>
        /// This method tries to get the ViewModel property of the current page in the frame.
        /// </summary>
        /// <param name="frame"> The frame, which contains the page, whose ViewModel property is to be retrieved. </param>
        /// <returns>The found ViewModel or null</returns>
        public static object GetPageViewModel(this Frame frame)
        {
            var frameContent = frame.Content;
            return frameContent?.GetType().GetProperty("ViewModel")?.GetValue(frameContent);
        }
    }
}
