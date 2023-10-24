var swiper = new Swiper(".slide-content", {
    slidesPerView: 4,
    spaceBetween: 25,
    // slidesPerGroup: 3,
    loop: true,
    // slideToClickedSlide: true,
    centeredSlides: true,
    fade: true,
    grabCursor: true,
    // centerInsufficientSlides: true,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
      dynamicBullets: true,
    },
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    breakpoints:{
      0: {
        slidesPerView: 1,
      },
      
      576: {
        slidesPerView: 2,
      },
      
      950: {
        slidesPerView:3,
      },

      1200: {
        slidesPerView:4,
      },
    }
  });