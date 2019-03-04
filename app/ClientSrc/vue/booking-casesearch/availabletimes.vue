<template>
    <div class="row">
        <div class="col-md-12">
            <div class="swiper-button-prev" slot="button-prev"></div>
            <swiper ref="mySwiper" :options="swiperOption" id="swipe-container">
                <swiper-slide v-for="entry in availabletimes" :key="entry.date">
                    <div class="custom-slide-container">
                        <div class="custom-slide-header">
                            {{ entry.weekday }}
                            <br />
                            {{ entry.formattedDate }}
                        </div>
                        <div class="custom-slide-times">
                            <div class="custom-slide-time" v-for="container in entry.times" @click="selectTime(container.containerId, container.startDateTime)"
                                 :class="{'selected': container.containerId === selectedContainerId}">
                                {{ container.start }} - {{ container.end }}
                            </div>
                        </div>
                    </div>
                </swiper-slide>
            </swiper>
            <div class="swiper-button-next" slot="button-next"></div>
        </div>
    </div>
</template>

<script>
    import Vue from "vue";
    import VueAwesomeSwiper from 'vue-awesome-swiper';
    import { swiper, swiperSlide } from 'vue-awesome-swiper';
    import axios from 'axios';

    Vue.use(VueAwesomeSwiper);

    export default {
        components: {
            swiper,
            swiperSlide
        },
        props: {
            locationId: Number,
            hearingType: Number
        },
        data() {
            return {
                availabletimes: [],
                selectedContainerId: null,
                selectedBookingTime: null,
                swiperOption: {
                    slidesPerView: 5,
                    centeredSlides: false,
                    spaceBetween: 20,
                    grabCursor: true,
                    pagination: {
                        el: '.swiper-pagination',
                        clickable: true
                    },
                    navigation: {
                        nextEl: '.swiper-button-next',
                        prevEl: '.swiper-button-prev'
                    },
                    breakpoints: {
                        1200: {
                            slidesPerView: 4,
                            spaceBetween: 18
                        },
                        1000: {
                            slidesPerView: 3,
                            spaceBetween: 16
                        },
                        780: {
                            slidesPerView: 2,
                            spaceBetween: 14
                        },
                        420: {
                            slidesPerView: 1,
                            spaceBetween: 12
                        }
                    }
                }
            }
        },
        methods: {
            selectTime(containerId, bookingTime) {
                this.selectedContainerId = containerId;
                this.selectedBookingTime = bookingTime;

                //check if date is still available
                validateCaseDate(containerId, this.convertToTicks(bookingTime));
            },
            convertToTicks(dt) {
                var date = new Date(dt);
                var currentTime = date.getTime();

                // 10,000 ticks in 1 millisecond
                // jsTicks is number of ticks from midnight Jan 1, 1970
                var jsTicks = currentTime * 10000;

                // add 621355968000000000 to jsTicks
                // netTicks is number of ticks from midnight Jan 1, 01 CE
                return ((jsTicks + 621355968000000000) - (date.getTimezoneOffset() * 600000000));
                
            }
        },
        created() {
            let self = this;
            axios.get(`/scjob/booking/api/available-dates-by-location/${this.locationId}/${this.hearingType}`)
                .then(response => {
                    self.availabletimes = response.data;
                });
        }
    }
</script>
