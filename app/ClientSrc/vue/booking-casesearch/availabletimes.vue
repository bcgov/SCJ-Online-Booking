<template>
    <div class="row no-gutters">
        <div class="col">
            <div class="swiper-button-prev" slot="button-prev"></div>
            <swiper ref="mySwiper" :options="swiperOption" id="swipe-container">
                <swiper-slide v-for="entry in availabletimes" :key="entry.date">
                    <div class="custom-slide-container">
                        <div class="custom-slide-header text-center">
                            {{ entry.weekday }}
                            <br />
                            {{ entry.formattedDate }}
                        </div>
                        <div class="custom-slide-times text-center">
                            <div v-for="container in entry.times">
                                <div class="btn-group" role="group">
                                    <button type="button" class="btn btn-tertiary btn-block"
                                            @click="selectTime(container.containerId, container.startDateTime)"
                                            :class="{'selected': container.containerId === selectedContainerId}">
                                        {{ container.start }} - {{ container.end }}
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </swiper-slide>
            </swiper>
            <div class="swiper-button-next" slot="button-next"></div>
            <input type="hidden" id="selectedDate" /> 
            <button type="button" class="btn btn-primary" id="slideBtn" hidden
                    @click="toSlide()"> 
                Slide to selected date
            </button>
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
            availableDates: [],
            hearingType: Number
        },
        data() {
            return {
                availabletimes: [],
                selectedContainerId: null,
                selectedBookingTime: null,
                swiperOption: {
                    slidesPerView: 4,
                    centeredSlides: false,
                    spaceBetween: 16,
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
                            spaceBetween: 16
                        },
                        1000: {
                            slidesPerView: 4,
                            spaceBetween: 16
                        },
                        780: {
                            slidesPerView: 3,
                            spaceBetween: 16
                        },
                        420: {
                            slidesPerView: 1,
                            spaceBetween: 16
                        }
                    }
                }
            }
        },
        methods: {
            toSlide() {
                const i = $('#selectedDate').val();
                this.$refs.mySwiper.swiper.slideTo(i, 0);
            },
            selectTime(containerId, bookingTime) {
                this.selectedContainerId = containerId;
                this.selectedBookingTime = bookingTime;

                //check if date is still available
                validateCaseDate(containerId, this.convertToTicks(bookingTime + 'Z'));
            },
            convertToTicks(dt) {
                var date = new Date(dt);
                var currentTime = date.getTime();

                // 10,000 ticks in 1 millisecond
                // jsTicks is number of ticks from midnight Jan 1, 1970
                var jsTicks = currentTime * 10000;

                // add 621355968000000000 to jsTicks
                // netTicks is number of ticks from midnight Jan 1, 01 CE
                return jsTicks + 621355968000000000;
                
            }
        },
        created() {
            let self = this;
            axios.get(`/scjob/booking/api/sc-available-dates-by-location/${this.locationId}/${this.hearingType}`)
                .then(response => {
                    self.availabletimes = response.data;
                });
        },
        mounted() {
            let self = this;
            this.$refs.mySwiper.swiper.on('slideChange', function () {
                $('#datepicker').val(self.availableDates[this.activeIndex]);
            });
        }
    }
</script>
