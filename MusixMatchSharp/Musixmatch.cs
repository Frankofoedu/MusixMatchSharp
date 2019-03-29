using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace MusixMatchSharp
{
    public class Musixmatch
    {
        private string __apikey { get; set; }

        private string __url { get; set; }

        public Musixmatch(string apikey)
        {
            __apikey = apikey;
            __url = "http://api.musixmatch.com/ws/1.1/";

        }

        private string _request(string url)
        {
            var webClt = new WebClient();
            var data = webClt.DownloadString(url);
            return data;
        }

        private string _get_url(string url)
        {
            return __url + string.Format("{0}&apikey={1}", url, __apikey);
        }


        private string _set_page_size(string page_size_int)
        {
            int i = Convert.ToInt32(page_size_int);

            if (i > 100)
                  i = 100;
            else if (i < 1)
                  i = 1;

            return i.ToString();
        }


        /// <summary>
        /// This api provides you the list of the top artists of a given country.
        /// </summary>
        /// <param name="page">Define the page number for paginated results.</param>
        /// <param name=" page_size">Define the page size for paginated results (range 1 - 100)</param>
        /// <param name="country">A valid country code(default US)</param>
        /// <param name="format">Decide the output type json or xml(default json)</param>

        /// <returns></returns>
        private string Chart_artists(string page, string page_size, string country = "us", string _format = "json")
        {

            var data = _request(this._get_url(string.Format("chart.artists.get?page={0}&page_size={1}&country={2}&format={3}", page, _set_page_size(page_size), country, _format)));
            return data;
        }

        /// <summary>
        /// This api provides you the list of the top songs of a given country.
        /// </summary>
        /// <param name="page">Define the page number for paginated results.</param>
        /// <param name="page_size">Define the page size for paginated results (range 1 - 100).</param>
        /// <param name="f_has_lyrics">When set, filter only contents with lyrics.</param>
        /// <param name="country">A valid country code (default US).</param>
        /// <param name="format">Decide the output type json or xml(default json).</param>
        /// <param name=""></param>
        /// <returns></returns>
        public string Chart_tracks_get(string page, string page_size, string f_has_lyrics, string country = "us", string _format = "json")
        {
            var data = _request(_get_url(string.Format("chart.tracks.get?''page={0}&page_size={1}''&country={2}&format={3}&f_has_lyrics={4}", page, _set_page_size(page_size), country, _format, f_has_lyrics)));
            return data;
        }

        /// <summary>
        /// Search for track in our database. *Requires commercial License
        /// </summary>
        /// <param name="q_track">The song title.</param>
        /// <param name="q_artist">The song artist.</param>
        /// <param name="page_size"> Allowed range is (0.1 – 0.9)</param>
        /// <param name="page">Define the page number for paginated results.</param>
        /// <param name="s_track_rating">Sort by our popularity index for tracks(asc|desc).</param>
        /// <param name="format">Decide the output type json or xml (default json).</param>
        public string Track_search(string q_track, string q_artist, string page_size, string page,
                    string s_track_rating, string _format = "json")
        {


            var data = _request(_get_url(string.Format("track.search?q_track={0}&q_artist={1}&page_size={2}&page={3}&s_track_rating={4}&format={5}", q_track, q_artist,
                                                       _set_page_size(page_size),
                                                       page, s_track_rating,
                                                       _format)));
            return data;
        }


        /// <summary>
        /// Get a track info from our database: title, artist, instrumental flag and cover art.
        /// </summary>
        /// <param name="track_id">The musiXmatch track id.</param>
        /// <param name="commontrack_id">The musiXmatch commontrack id</param>
        /// <param name="track_isrc">A valid ISRC identifier</param>
        /// <param name="track_mbid">The musicbrainz recording id</param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Track_get(string track_id, string commontrack_id = null,
                  string track_isrc = null, string track_mbid = null, string _format = "json")
        {
            var data = _request(_get_url(string.Format("track.get?track_id={0}&commontrack_id={1}&track_isrc={2}&track_mbid={3}&format={4}", track_id, commontrack_id,
                                                    track_isrc, track_mbid,
                                                    _format)));
            return data;
        }

        /// <summary>
        /// Get the lyrics of a track.
        /// </summary>
        /// <param name="track_id">The musiXmatch track id</param>
        /// <param name="track_mbid">The musicbrainz track id</param>
        /// <param name="format">Decide the output type json or xml(default json).</param>
        /// <returns></returns>
        public string Track_lyrics_get(string track_id, string track_mbid = "0", string _format = "json")
        {

            var data = _request(_get_url(string.Format("track.lyrics.get?track_id={0}&track_mbid={1}&format={2}", track_id, track_mbid, _format)));
            return data;
        }

        /// <summary>
        /// Get the snippet for a given track.
        ///A lyrics snippet is a very short representation of a song lyrics.
        ///It’s usually twenty to a hundred characters long and it’s calculated
        ///extracting a sequence of words from the lyrics.
        /// </summary>
        /// <param name="track_id">The musiXmatch track id</param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Track_snippet_get(string track_id, string _format = "json")
        {
            var data = _request(_get_url(string.Format("track.snippet.get?track_id={0}&format={1}", track_id, _format)));
            return data;
        }


        /// <summary>
        /// Retreive the subtitle of a track.
        ///Return the subtitle of a track in LRC or DFXP format.
        ///Refer to Wikipedia LRC format page or DFXP format on W3c
        /// for format specifications.
        /// </summary>
        /// <param name="track_id">The musiXmatch track id</param>
        /// <param name="track_mbid">The musicbrainz track id</param>
        /// <param name="subtitle_format">The format of the subtitle (lrc,dfxp,stledu). Default to lrc.</param>
        /// <param name="f_subtitle_length">The desired length of the subtitle (seconds)</param>
        /// <param name="f_subtitle_length_max_deviation">The maximum deviation allowed. from the f_subtitle_length(seconds)</param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Track_subtitle_get(string track_id, string track_mbid = null,
                           string subtitle_format = null, string f_subtitle_length = null,
                          string f_subtitle_length_max_deviation = null,
                          string _format = "json")
        {
            var data = _request(_get_url(string.Format("track.subtitle.get?track_id={0}&track_mbid={1}subtitle_format={2}&f_subtitle_length={3}&f_subtitle_length_max_deviation={4}&format={5}", track_id,
                                                 track_mbid,
                                                 subtitle_format,
                                                 f_subtitle_length,
                                                 f_subtitle_length_max_deviation,
                                                 _format)));
            return data;
        }


        /// <summary>
        /// Get the Rich sync for a track.
        ///  A rich sync is an enhanced version of the
        /// standard sync which allows:
        /// </summary>
        /// <param name="track_id">The musiXmatch track id.</param>
        /// <param name="f_sync_length">The desired length of the sync (seconds).</param>
        /// <param name="f_sync_length_max_deviation">The maximum deviation allowed. from the f_sync_length (seconds)</param>
        /// <param name="_format"></param>
        /// <returns></returns>
        public string Track_richsync_get(string track_id, string f_sync_length = null,
                          string f_sync_length_max_deviation = null, string _format = "json")
        {
            var data = _request(_get_url(string.Format("track.richsync.get?track_id={0}&f_sync_length={1}&f_sync_length_max_deviation={2}&format={3}",
                                            track_id, f_sync_length,
                                                    f_sync_length_max_deviation,
                                                    _format)));
            return data;

        }


        /// <summary>
        /// Submit a lyrics to our database.
        /// </summary>
        /// <param name="track_id">A valid country code (default US)</param>
        /// <param name="lyrics_body">The lyrics</param>
        /// <param name="_format">the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Track_lyrics_post(string track_id,
                          string lyrics_body, string _format = "json")
        {
            var data = _request(_get_url(string.Format("track.lyrics.post?track_id={0}&lyrics_body={1}&format={2}", track_id, lyrics_body, _format)));

            return data;
        }

        /// <summary>
        /// This API method provides you the opportunity to help
        /// us improving our catalogue.
        ///  We aim to provide you with the best quality service imaginable,
        ///  so we are especially interested in your detailed feedback to help
        ///   us to continually improve it.
        ///   Please take all the necessary precautions to avoid users or
        /// automatic software to use your website/app to use this commands,
        /// a captcha solution like http://www.google.com/recaptcha or an
        /// equivalent one has to be implemented in every user interaction that
        /// ends in a POST operation on the musixmatch api.
        /// </summary>
        /// <param name="track_id">The musiXmatch track id</param>
        /// <param name="lyrics_id">The musiXmatch lyrics id</param>
        /// <param name="feedback">The feedback to be reported, possible values are
        /// rong_lyrics, wrong_attribution, bad_characters,
        /// lines_too_long, wrong_verses, wrong_formatting
        /// </param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Track_lyrics_feedback_post(string track_id, string lyrics_id,
                                  string feedback, string _format = "json")
        {
            var data = _request(_get_url(string.Format("track.lyrics.feedback.post?track_id={0}&lyrics_id={1}&feedback={2}&format={3}", track_id, lyrics_id,
                                                   feedback, _format)));
            return data;
        }

        /// <summary>
        /// Get the lyrics for track based on title and artist.
        /// </summary>
        /// <param name="q_track">The song title</param>
        /// <param name="q_artist">The song artist</param>
        /// <param name="_format"> Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Matcher_lyrics_get(string q_track, string q_artist, string _format = "json")
        {
            var data = _request(_get_url(string.Format("matcher.lyrics.get?q_track={0}&q_artist={1}&format={2}", q_track, q_artist,
                                                     _format)));
            return data;

        }

        /// <summary>
        /// Match your song against our database.
        /// </summary>
        /// <param name="q_track">The song title</param>
        /// <param name="q_artist">The song artist</param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Matcher_track_get(string q_track, string q_artist, string _format = "json")
        {
            var data = _request(_get_url(string.Format("matcher.track.get?q_track={0}&q_artist={1}&format={2}", q_track, q_artist,
                                                   _format)));
            return data;
        }


        /// <summary>
        /// Get the subtitles for a song given his title,artist and duration.
        /// </summary>
        /// <param name="q_track">The song title</param>
        /// <param name="q_artist">The song artist</param>
        /// <param name="f_subtitle_length">Filter by subtitle length in seconds</param>
        /// <param name="f_subtitle_length_max_deviation"> Max deviation for a subtitle length in seconds</param>
        /// <param name="track_isrc">If you have an available isrc id in your catalogue you can query using this id only (optional)</param>
        /// <param name="_format"></param>
        /// <returns></returns>
        public string Matcher_subtitle_get(string q_track, string q_artist, string f_subtitle_length,
                            string f_subtitle_length_max_deviation, string track_isrc = null,
                            string _format = "json")
        {
            var data = _request(_get_url(string.Format("matcher.subtitle.get?q_track={0}&q_artist={1}&f_subtitle_length={2}&f_subtitle_length_max_deviation={3}&track_isrc={4}&format={5}", q_track, q_artist,
                                                f_subtitle_length,
                                                f_subtitle_length_max_deviation,
                                                track_isrc,
                                                _format)));
            return data;
        }

        /// <summary>
        /// Get the artist data from our database.
        /// </summary>
        /// <param name="artist_id">Musixmatch artist id.</param>
        /// <param name="artist_mbid">Musicbrainz artist id</param>
        /// <param name="_format">Decide the output type json or xml (default json).</param>
        /// <returns></returns>
        public string Artist_get(string artist_id, string artist_mbid = null, string _format = "json")
        {
            var data = _request(_get_url(string.Format("artist.get?artist_id={0}&artist_mbid={1}&format={2}", artist_id, artist_mbid,
                                                     _format)));
            return data;
        }


        /// <summary>
        /// Search for artists in our database.
        /// </summary>
        /// <param name="q_artist">The song artist</param>
        /// <param name="page">Define the page number for paginated results</param>
        /// <param name="page_size">Define the page size for paginated results (Range is 1 to 100).</param>
        /// <param name="f_artist_id">When set, filter by this artist id</param>
        /// <param name="f_artist_mbid">When set, filter by this artist musicbrainz id</param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Artist_search(string q_artist, string page, string page_size, string f_artist_id,
                     string f_artist_mbid,
                     string _format = "json")
        {
            var data = _request(_get_url(string.Format("artist.search?q_artist={0}&f_artist_id={1}&f_artist_mbid={2}&page={3}&page_size={4}&format={5}"
                                         , q_artist, f_artist_id,
                                                  f_artist_mbid, page,
                                                  _set_page_size(page_size),
                                                  _format)));
            return data;
        }

        /// <summary>
        /// Get the album discography of an artist.
        /// </summary>
        /// <param name="artist_id">Musixmatch artist id.</param>
        /// <param name="g_album_name">Group by Album Name.</param>
        /// <param name="page">Define the page number for paginated results.</param>
        /// <param name="page_size">Define the page size for paginated results</param>
        /// <param name="s_release_date">Sort by release date (asc|desc).</param>
        /// <param name="artist_mbid">Musicbrainz artist id.</param>
        /// <param name="_format">Decide the output type json or xml (default json).</param>
        /// <returns></returns>
        public string Artist_albums_get(string artist_id, string g_album_name, string page, string page_size,
                         string s_release_date, string artist_mbid = null, string _format = "json")
        {
            var data = _request(_get_url(string.Format("artist.albums.get?artist_id={0}&artist_mbid={1}&g_album_name={2}&s_release_date={3}&page={4}&page_size={5}&format={6}",
                                            artist_id, artist_mbid,
                                                    g_album_name,
                                                    s_release_date,
                                                    page,
                                                    _set_page_size(page_size),
                                                    _format)));
            return data;
        }


        /// <summary>
        /// Get a list of artists somehow related to a given one.
        /// </summary>
        /// <param name="artist_id">Musixmatch artist id</param>
        /// <param name="page">Define the page number for paginated results</param>
        /// <param name="page_size">Define the page size for paginated results (range is 1 to 100)</param>
        /// <param name="artist_mbid"> Musicbrainz artist id</param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Artist_related_get(string artist_id, string page, string page_size,
                          string artist_mbid = null, string _format = "json")
        {
            var data = _request(_get_url(string.Format("artist.related.get?artist_id={0}&artist_mbid={1}&page={2}&page_size={3}&format={4}", artist_id,
                                                  artist_mbid, page,
                                                  _set_page_size(page_size),
                                                  _format)));
            return data;
        }


        /// <summary>
        /// Get an album from our database: name, release_date, release_type, cover art.
        /// </summary>
        /// <param name="album_id">The musiXmatch album id.</param>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Album_get(string album_id, string _format = "json")
        {
            var data = _request(_get_url(string.Format("album.get?album_id={0}&format={1}", album_id, _format)));
            return data;
        }

        /// <summary>
        /// This api provides you the list of the songs of an album.
        /// </summary>
        /// <param name="album_id"> Musixmatch album id.</param>
        /// <param name="page">Define the page number for paginated results.</param>
        /// <param name="page_size">Define the page size for paginated results. (range is 1 to 100)</param>
        /// <param name="album_mbid"> Musicbrainz album id.</param>
        /// <param name="f_has_lyrics">When set, filter only contents with lyrics.</param>
        /// <param name="_format">Decide the output type json or xml (default json).</param>
        /// <returns></returns>
        public string Album_tracks_get(string album_id, string page, string page_size, string album_mbid,
                        string f_has_lyrics = null, string _format = "json")
        {
            var data = _request(_get_url(string.Format("album.tracks.get?album_id={0}&album_mbid={1}&f_has_lyrics={2}&page={3}&page_size={4}&format={5}",
                                            album_id, album_mbid,
                                                    f_has_lyrics, page,
                                                    _set_page_size(page_size),
                                                    _format)));
            return data;
        }

        /// <summary>
        /// Get the base url for the tracking script.With this api you’ll be able to get the base
        /// url for the tracking script you need to insert in
        /// your page to legalize your existent lyrics library.
        /// Read more here: rights-clearance-on-your-existing-catalog
        /// In case you’re fetching the lyrics by the musiXmatch api
        /// called track.lyrics.get you don’t need to implement this API call.
        /// </summary>
        /// <param name="domain">Your domain name.</param>
        /// <param name="_format">Decide the output type json or xml (default json).</param>
        /// <returns></returns>
        public string Tracking_url_get(string domain, string _format = "json")
        {
            var data = _request(_get_url(string.Format("tracking.url.get?domain={0}&format={1}", domain, _format)));
            return data;
        }

        /// <summary>
        /// Get the list of our songs with the lyrics last updated information.
        /// CATALOGUE_COMMONTRACKS
        /// Dump of our catalogue in this format:
        /// {
        ///        "track_name": "Shape of you",
        ///     "artist_name": "Ed Sheeran",
        ///     "commontrack_id":12075763,
        ///       "instrumental": false,
        ///       "has_lyrics": yes,
        ///       "updated_time": "2013-04-08T09:28:40Z"
        ///      }
        /// Note: This method requires a commercial plan.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Catalogue_dump_get(string url)
        {
            var data = _request(_get_url(url));
            return data;
        }

        /// <summary>
        /// Get the list of the music genres of our catalogue:
        /// music_genre_id, music_genre_parent_id, music_genre_name, music_genre_name_extended, music_genre_vanity
        /// </summary>
        /// <param name="_format">Decide the output type json or xml (default json)</param>
        /// <returns></returns>
        public string Genres_get(string _format = "json")
        {
            var data = _request(_get_url(string.Format("music.genres.get?format={0}",_format)));
            return data;
        }
    }
}
