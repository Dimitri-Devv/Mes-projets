export const useRandomVideo = () => {
  const videos = [
    "/videos/bg-login-joker.mp4",
    "/videos/bg-login-voldemort.mp4",
    "/videos/bg-login-ironman.mp4",
    "/videos/bg-login-starwars.mp4",
    "/videos/bg-login-spiderman.mp4",
    "/videos/bg-login-johnwick.mp4",
  ];

  return videos[Math.floor(Math.random() * videos.length)];
};
